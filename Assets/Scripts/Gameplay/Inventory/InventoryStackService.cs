using System.Collections.Generic;
using UnityEngine;
public interface IInventoryStackService
{
    void FillExistingStacks(
       Dictionary<InventoryItemId, InventoryItem> items,
       ItemId itemId,
       ItemConfig config,
       ref int count,
       ref InventoryItem lastUpdatedItem);

    bool TryMergeStacks(
       InventoryItem sourceItem,
       InventoryItem targetItem,
       ItemConfig config,
       out bool sourceRemoved);
}

public sealed class InventoryStackService : IInventoryStackService
{
    public void FillExistingStacks(
       Dictionary<InventoryItemId, InventoryItem> items,
       ItemId itemId,
       ItemConfig config,
       ref int count,
       ref InventoryItem lastUpdatedItem)
    {
        foreach (InventoryItem existing in items.Values)
        {
            if (!Equals(existing.ItemId, itemId))
                continue;

            int freeSpace = config.MaxStack - existing.Count;
            if (freeSpace <= 0)
                continue;

            int toAdd = Mathf.Min(freeSpace, count);
            existing.AddToStack(toAdd);
            count -= toAdd;
            lastUpdatedItem = existing;

            if (count == 0)
                break;
        }
    }

    public bool TryMergeStacks(
       InventoryItem sourceItem,
       InventoryItem targetItem,
       ItemConfig config,
       out bool sourceRemoved)
    {
        sourceRemoved = false;

        if (!config.IsStackable())
            return false;

        if (!Equals(sourceItem.ItemId, targetItem.ItemId))
            return false;

        if (targetItem.Count >= config.MaxStack)
            return false;

        int availableSpace = config.MaxStack - targetItem.Count;
        int moveCount = Mathf.Min(availableSpace, sourceItem.Count);

        if (moveCount <= 0)
            return false;

        targetItem.AddToStack(moveCount);

        if (moveCount == sourceItem.Count)
        {
            sourceRemoved = true;
            return true;
        }

        sourceItem.RemoveFromStack(moveCount);
        return true;
    }
}