public class InventoryCellViewData
{
    public int X { get; private set; }
    public int Y { get; private set; }

    public bool IsOccupied { get; private set; }
    public bool IsRoot { get; private set; }

    public InventoryCellViewData(int x, int y)
    {
        X = x;
        Y = y;
    }

    public void Bind(InventoryCell model)
    {
        IsOccupied = !model.IsEmpty;
        IsRoot = model.IsRoot;
    }
}