public readonly struct InventoryDropTargetResult
{
    public static InventoryDropTargetResult None => new(null);

    public readonly IInventoryDropTarget Target;

    public bool HasTarget => Target != null;
    public InventoryDropTargetType Type => Target?.TargetType ?? InventoryDropTargetType.None;

    public InventoryDropTargetResult(IInventoryDropTarget target)
    {
        Target = target;
    }
}
