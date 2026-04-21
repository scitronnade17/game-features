using System.Collections.Generic;
public class InventoryItem
{
    public InventoryItemId InventoryId { get; }
    public ItemId ItemId { get; }

    public int Width { get; }
    public int Height { get; }

    public int RootX { get; private set; }
    public int RootY { get; private set; }

    public int Count { get; private set; } = 1;

    public InventoryItem(
      InventoryItemId inventoryId,
      ItemId itemId,
      int width,
      int height,
      int rootX,
      int rootY)
    {
        InventoryId = inventoryId;
        ItemId = itemId;
        Width = width;
        Height = height;
        RootX = rootX;
        RootY = rootY;
    }

    public void AddToStack(int amount)
      => Count += amount;

    public void RemoveFromStack(int amount)
    {
        Count -= amount;
        if (Count < 0)
            Count = 0;
    }

    public void SetRootPosition(int x, int y)
    {
        RootX = x;
        RootY = y;
    }

    public IEnumerable<(int x, int y)> GetOccupiedCells()
    {
        for (int deltaX = 0; deltaX < Width; deltaX++)
            for (int deltaY = 0; deltaY < Height; deltaY++)
                yield return (RootX + deltaX, RootY + deltaY);
    }
}