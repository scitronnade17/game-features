public readonly struct DroppedItemDescriptor
{
    public InventoryItemId Id { get; }
    public ItemId ItemId { get; }


    public DroppedItemDescriptor(InventoryItemId id, ItemId itemId)
    {
        Id = id;
        ItemId = itemId;
    }
}