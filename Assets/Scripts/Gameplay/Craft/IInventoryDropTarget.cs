public interface ICraftingDropTarget : IInventoryDropTarget
{
    void TryPut(InventoryItemId inventoryItemId);
}

public interface IInventoryDropTarget
{
    InventoryDropTargetType TargetType { get; }
}
