using UnityEngine;

public sealed class CraftingSlot
{
    public ItemId ItemId { get; }
    public int MaxCount { get; }

    public InventoryItem Item { get; private set; }
    public int CurrentCount { get; private set; }

    public CraftingSlot(ItemId itemId, int maxCount)
    {
        ItemId = itemId;
        MaxCount = maxCount;
    }

    public void Remove(int count)
    {
        CurrentCount = Mathf.Max(0, CurrentCount - count);
        if (CurrentCount == 0)
            Item = null;
    }

    public bool CanAccept(InventoryItem item, int count)
    {
        if (item == null)
            return false;

        if (!Equals(item.ItemId, ItemId))
            return false;

        if (CurrentCount + count > MaxCount)
            return false;

        return true;
    }

    public bool TryPut(InventoryItem item, int count)
    {
        if (!CanAccept(item, count))
            return false;

        Item = item;
        CurrentCount += count;
        return true;
    }

    public void Clear()
    {
        Item = null;
        CurrentCount = 0;
    }
}
