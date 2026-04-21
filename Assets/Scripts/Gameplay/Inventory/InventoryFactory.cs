using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public interface IInventoryFactory
{
    InventoryCellView CreateInventoryCell(Transform parent);
    InventoryItemView CreateInventoryItemView(InventoryItem item, Transform parent, Vector2 cellSize, float canvasScaleFactor);
}

public class InventoryFactory : IInventoryFactory
{
    private readonly IConfigDataService configs;
    private readonly IDIService di;

    public InventoryFactory(IConfigDataService _configs,
      IDIService _di)
    {
        configs = _configs;
        di = _di;
    }

    public InventoryCellView CreateInventoryCell(Transform parent)
    {
        var gameConfig = configs.GetInventoryConfig();
        var cellViewObject = Object.Instantiate(gameConfig.InventoryCellViewPrefab, parent);
        var cellView = cellViewObject.GetComponent<InventoryCellView>();
        return cellView;
    }

    public InventoryItemView CreateInventoryItemView(InventoryItem item, Transform parent, Vector2 cellSize, float canvasScaleFactor)
    {
        var gameConfig = configs.GetInventoryConfig();
        var config = configs.GetItemConfig(item.ItemId);
        var itemViewObject = Object.Instantiate(gameConfig.InventoryViewPrefab, parent);
        var inventoryItemView = itemViewObject.GetComponent<InventoryItemView>();
        di.Container.InjectGameObject(itemViewObject.gameObject);

        inventoryItemView.Setup(item.InventoryId,
          item.Count,
          config.Icon,
          item.Width,
          item.Height,
          new Vector2Int(item.RootX, item.RootY),
          cellSize,
          canvasScaleFactor
        );

        return inventoryItemView;
    }
}