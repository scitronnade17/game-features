using System;
using System.Collections.Generic;
using UnityEngine;

public interface IInventoryPanelService
{
    event Action<InventoryCellViewData[,], int, int, Vector2> OnBuildGridBackground;
    event Action<IReadOnlyDictionary<InventoryItemId, InventoryItemViewData>> OnUpdateItemViews;
    InventoryCellViewData[,] GetCells();
    IReadOnlyDictionary<InventoryItemId, InventoryItemViewData> GetItemViews();
    void UpdateItemViews();
    void BuildGridBackground();
}

public class InventoryPanelService : IInventoryPanelService, IDisposable
{
    public event Action<InventoryCellViewData[,], int, int, Vector2> OnBuildGridBackground;
    public event Action<IReadOnlyDictionary<InventoryItemId, InventoryItemViewData>> OnUpdateItemViews;

    private readonly IInventoryService inventory;
    private readonly IConfigDataService itemConfigs;
    private readonly IInventoryFactory inventoryFactory;
    private readonly IEventBus eventBus;

    private InventoryCellViewData[,] cellViewData;
    private Dictionary<InventoryItemId, InventoryItemViewData> itemViews = new();

    public InventoryPanelService(
       IInventoryService _inventory,
       IConfigDataService _itemConfigs,
       IInventoryFactory _inventoryFactory,
       IEventBus _eventBus)
    {
        inventory = _inventory;
        itemConfigs = _itemConfigs;
        inventoryFactory = _inventoryFactory;
        eventBus = _eventBus;

        inventory.OnInventoryChanged += UpdateItemViews;
    }

    public InventoryCellViewData[,] GetCells() => cellViewData;
    public IReadOnlyDictionary<InventoryItemId, InventoryItemViewData> GetItemViews() => itemViews;

    public void BuildGridBackground()
    {
        int width = inventory.Width;
        int height = inventory.Height;

        cellViewData = new InventoryCellViewData[width, height];

        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
                cellViewData[x, y] = new InventoryCellViewData(x, y);

        OnBuildGridBackground?.Invoke(GetCells(), width, height, inventory.CellSize);
    }

    public void UpdateItemViews()
    {
        itemViews.Clear();

        foreach (var item in inventory.GetItems())
            itemViews[item.InventoryId] = new InventoryItemViewData(item);

        OnUpdateItemViews?.Invoke(GetItemViews());
    }

    public void Dispose()
    {
        inventory.OnInventoryChanged -= UpdateItemViews;
    }
}