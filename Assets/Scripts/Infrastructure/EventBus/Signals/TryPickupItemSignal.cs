using UnityEngine;

public struct TryPickupItemSignal
{
    public ItemId ItemId { get; }
    public ItemWorld ItemWorld { get; }
    public int Count { get; }

    public TryPickupItemSignal(ItemId _itemId, ItemWorld _itemWorld, int _count)
    {
        ItemId = _itemId;
        Count = _count;
        ItemWorld = _itemWorld;
    }
}