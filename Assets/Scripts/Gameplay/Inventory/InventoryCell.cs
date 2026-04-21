public class InventoryCell
{
    public int X { get; }
    public int Y { get; }

    public InventoryItem Item { get; internal set; }
    public bool IsRoot { get; internal set; }

    public bool IsEmpty => Item == null;

    public InventoryCell(int x, int y)
    {
        X = x;
        Y = y;
    }
}