public readonly struct CraftingIngredient
{
    public ItemId ItemId { get; }
    public int Count { get; }

    public CraftingIngredient(ItemId itemId, int count)
    {
        ItemId = itemId;
        Count = count;
    }
}
