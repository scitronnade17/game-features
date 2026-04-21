public interface IInventoryGridService
{
    bool IsInside(InventoryCell[,] cells, int width, int height, int x, int y);
    bool CanPlace(InventoryCell[,] cells, int width, int height, int itemWidth, int itemHeight, int rootX, int rootY);
    bool TryFindFreePlace(InventoryCell[,] cells, int width, int height, int itemWidth, int itemHeight, out int placeX, out int placeY);
    void PlaceItem(InventoryCell[,] cells, InventoryItem item, int rootX, int rootY);
    void ClearItemCells(InventoryCell[,] cells, int width, int height, InventoryItem item);
}

public sealed class InventoryGridService : IInventoryGridService
{
    public bool IsInside(InventoryCell[,] cells, int width, int height, int x, int y) =>
       x >= 0 && y >= 0 && x < width && y < height;

    public bool CanPlace(InventoryCell[,] cells, int width, int height, int itemWidth, int itemHeight, int rootX, int rootY)
    {
        if (!IsInside(cells, width, height, rootX, rootY))
            return false;

        if (!IsInside(cells, width, height, rootX + itemWidth - 1, rootY + itemHeight - 1))
            return false;

        for (int dx = 0; dx < itemWidth; dx++)
            for (int dy = 0; dy < itemHeight; dy++)
            {
                int x = rootX + dx;
                int y = rootY + dy;

                if (!cells[x, y].IsEmpty)
                    return false;
            }

        return true;
    }

    public bool TryFindFreePlace(InventoryCell[,] cells, int width, int height, int itemWidth, int itemHeight, out int placeX, out int placeY)
    {
        for (int y = 0; y < height; y++)
            for (int x = 0; x < width; x++)
            {
                if (CanPlace(cells, width, height, itemWidth, itemHeight, x, y))
                {
                    placeX = x;
                    placeY = y;
                    return true;
                }
            }

        placeX = default;
        placeY = default;
        return false;
    }

    public void PlaceItem(InventoryCell[,] cells, InventoryItem item, int rootX, int rootY)
    {
        for (int deltaX = 0; deltaX < item.Width; deltaX++)
            for (int deltaY = 0; deltaY < item.Height; deltaY++)
            {
                int x = rootX + deltaX;
                int y = rootY + deltaY;

                InventoryCell cell = cells[x, y];
                cell.Item = item;
                cell.IsRoot = deltaX == 0 && deltaY == 0;
            }

        item.SetRootPosition(rootX, rootY);
    }

    public void ClearItemCells(InventoryCell[,] cells, int width, int height, InventoryItem item)
    {
        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
            {
                InventoryCell cell = cells[x, y];
                if (cell.Item == item)
                {
                    cell.Item = null;
                    cell.IsRoot = false;
                }
            }
    }
}