using System;
using System.Collections.Generic;
using UnityEngine;

public interface IInventoryService
{
    event Action OnInventoryChanged;

    int Width { get; }
    int Height { get; }
    Vector2 CellSize { get; }

    InventoryCell[,] GetCells();
    IEnumerable<InventoryItem> GetItems();
    InventoryItem GetItemAtCell(int x, int y);
    InventoryItem GetItemById(InventoryItemId inventoryItemId);

    InventoryResult TryAddNewItem(ItemId itemId, int count, out InventoryItem addedItem);
    InventoryResult TryMoveItem(InventoryItemId id, int newX, int newY);
    InventoryResult TryRemoveItem(InventoryItemId id, out InventoryItem removedItem);
    InventoryResult TryDropItem(InventoryItemId id, out DroppedItemDescriptor droppedItem);
}

public sealed class InventoryService : IInventoryService
{
    public event Action OnInventoryChanged;

    public int Width { get; }
    public int Height { get; }
    public Vector2 CellSize { get; }

    private readonly InventoryCell[,] cells;
    private readonly Dictionary<InventoryItemId, InventoryItem> items = new();

    private readonly IConfigDataService itemConfigs;
    private readonly IInventoryGridService gridService;
    private readonly IInventoryStackService stackService;

    public InventoryService(
       IConfigDataService _itemConfigs,
       IInventoryGridService _gridService,
       IInventoryStackService _stackService)
    {
        itemConfigs = _itemConfigs;
        gridService = _gridService;
        stackService = _stackService;

        Width = 10;
        Height = 5;
        CellSize = new Vector2(40, 40);

        cells = new InventoryCell[Width, Height];

        for (int x = 0; x < Width; x++)
            for (int y = 0; y < Height; y++)
                cells[x, y] = new InventoryCell(x, y);
    }

    public InventoryCell[,] GetCells() => cells;
    public IEnumerable<InventoryItem> GetItems() => items.Values;

    public InventoryItem GetItemAtCell(int x, int y)
    {
        if (!gridService.IsInside(cells, Width, Height, x, y))
            return null;

        return cells[x, y].Item;
    }

    public InventoryItem GetItemById(InventoryItemId inventoryItemId) =>
       items.TryGetValue(inventoryItemId, out InventoryItem item) ? item : null;

    public InventoryResult TryAddNewItem(ItemId itemId, int count, out InventoryItem addedItem)
    {
        addedItem = null;

        if (count <= 0)
            return InventoryResult.Fail(InventoryFailReason.InvalidCount);

        if (!TryGetItemConfig(itemId, out ItemConfig config))
            return InventoryResult.Fail(InventoryFailReason.ItemConfigNotFound);

        int itemWidth = config.GridSize.x;
        int itemHeight = config.GridSize.y;

        if (config.IsStackable())
            stackService.FillExistingStacks(items, itemId, config, ref count, ref addedItem);

        if (count > 0)
        {
            InventoryResult createResult = TryCreateNewStacks(
               itemId,
               config,
               itemWidth,
               itemHeight,
               ref count,
               ref addedItem);

            if (createResult.IsFail)
                return createResult;
        }

        if (count > 0)
            return InventoryResult.Fail(InventoryFailReason.NoFreeSpace);

        NotifyChanged();
        return InventoryResult.Success();
    }

    public InventoryResult TryMoveItem(InventoryItemId id, int newX, int newY)
    {
        if (!items.TryGetValue(id, out InventoryItem moveItem))
            return InventoryResult.Fail(InventoryFailReason.ItemNotFound);

        if (!gridService.IsInside(cells, Width, Height, newX, newY))
            return InventoryResult.Fail(InventoryFailReason.OutOfBounds);

        InventoryItem targetItem = cells[newX, newY].Item;

        if (targetItem == null || targetItem.InventoryId == moveItem.InventoryId)
            return TryRelocateItem(moveItem, newX, newY);

        return TryMergeItemIntoStack(id, moveItem, targetItem);
    }

    public InventoryResult TryDropItem(InventoryItemId id, out DroppedItemDescriptor droppedItem)
    {
        droppedItem = default;

        InventoryResult removeResult = TryRemoveItem(id, out InventoryItem removedItem);
        if (removeResult.IsFail)
            return removeResult;

        droppedItem = new DroppedItemDescriptor(id, removedItem.ItemId);
        return InventoryResult.Success();
    }


    private InventoryResult TryRelocateItem(InventoryItem moveItem, int newX, int newY)
    {
        int oldX = moveItem.RootX;
        int oldY = moveItem.RootY;

        gridService.ClearItemCells(cells, Width, Height, moveItem);

        bool canPlace = gridService.CanPlace(
           cells,
           Width,
           Height,
           moveItem.Width,
           moveItem.Height,
           newX,
           newY);

        if (!canPlace)
        {
            gridService.PlaceItem(cells, moveItem, oldX, oldY);
            return InventoryResult.Fail(InventoryFailReason.PlaceBlocked);
        }

        gridService.PlaceItem(cells, moveItem, newX, newY);

        NotifyChanged();
        return InventoryResult.Success();
    }

    public InventoryResult TryRemoveItem(InventoryItemId id, out InventoryItem removedItem)
    {
        removedItem = null;

        if (!items.TryGetValue(id, out InventoryItem item))
            return InventoryResult.Fail(InventoryFailReason.ItemNotFound);

        gridService.ClearItemCells(cells, Width, Height, item);
        items.Remove(id);

        removedItem = item;

        NotifyChanged();
        return InventoryResult.Success();
    }


    private InventoryResult TryMergeItemIntoStack(InventoryItemId sourceId, InventoryItem sourceItem,
       InventoryItem targetItem)
    {
        if (!TryGetItemConfig(sourceItem.ItemId, out ItemConfig config))
            return InventoryResult.Fail(InventoryFailReason.ItemConfigNotFound);

        bool merged = stackService.TryMergeStacks(sourceItem, targetItem, config, out bool sourceRemoved);
        if (!merged)
            return InventoryResult.Fail(InventoryFailReason.StackMergeFailed);

        if (sourceRemoved)
        {
            gridService.ClearItemCells(cells, Width, Height, sourceItem);
            items.Remove(sourceId);
        }

        NotifyChanged();
        return InventoryResult.Success();
    }

    private InventoryResult TryCreateNewStacks(
       ItemId itemId,
       ItemConfig config,
       int itemWidth,
       int itemHeight,
       ref int count,
       ref InventoryItem lastCreatedItem)
    {
        while (count > 0)
        {
            bool hasFreePlace = gridService.TryFindFreePlace(
               cells,
               Width,
               Height,
               itemWidth,
               itemHeight,
               out int placeX,
               out int placeY);

            if (!hasFreePlace)
                return InventoryResult.Fail(InventoryFailReason.NoFreeSpace);

            InventoryItemId instanceId = InventoryItemId.New();
            InventoryItem item = new InventoryItem(instanceId, itemId, itemWidth, itemHeight, placeX, placeY);

            bool isStackable = config.IsStackable();
            if (isStackable)
            {
                int stackSize = Mathf.Min(config.MaxStack, count);

                if (stackSize > 1)
                    item.AddToStack(stackSize - 1);

                count -= stackSize;
            }
            else
            {
                count -= 1;
            }

            gridService.PlaceItem(cells, item, placeX, placeY);
            items[instanceId] = item;
            lastCreatedItem = item;
        }

        return InventoryResult.Success();
    }

    private bool TryGetItemConfig(ItemId itemId, out ItemConfig config)
    {
        config = itemConfigs.GetItemConfig(itemId);
        return config != null;
    }

    private void NotifyChanged() =>
       OnInventoryChanged?.Invoke();

    public InventoryResult TryConsumeItem(InventoryItemId id, int amount, out InventoryItem changedItem)
    {
        changedItem = null;

        if (amount <= 0)
            return InventoryResult.Fail(InventoryFailReason.InvalidConsumeAmount);

        if (!items.TryGetValue(id, out InventoryItem item))
            return InventoryResult.Fail(InventoryFailReason.ItemNotFound);

        if (item.Count < amount)
            return InventoryResult.Fail(InventoryFailReason.NotEnoughItemsInStack);

        item.RemoveFromStack(amount);

        if (item.Count <= 0)
        {
            gridService.ClearItemCells(cells, Width, Height, item);
            items.Remove(id);
            NotifyChanged();
            return InventoryResult.Success();
        }

        changedItem = item;
        NotifyChanged();
        return InventoryResult.Success();
    }
}