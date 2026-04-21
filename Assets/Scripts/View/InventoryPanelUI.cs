using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class InventoryPanelUI : MonoBehaviour
{
    [SerializeField] private GridLayoutGroup grid;
    [SerializeField] private RectTransform itemsRoot;
    [SerializeField] private Canvas canvas;

    private IInventoryPanelPresenter presenter;
    private IInventoryFactory inventoryFactory;
    private InventoryCellView[,] cellViews;
    private List<InventoryItemView> itemViews = new();

    [Inject]
    public void Construct(IInventoryFactory _inventoryFactory,
      IInventoryPanelPresenter _presenter)
    {
        inventoryFactory = _inventoryFactory;
        presenter = _presenter;

        presenter.OnBuildGridBackground += BuildGrid;
        presenter.OnUpdateInventoryViewOld += UpdateItemViews;
    }

    private void BuildGrid(InventoryCellViewData[,] cells, int width, int height, Vector2 cellSize)
    {
        grid.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        grid.constraintCount = width;
        grid.cellSize = new Vector2Int((int)cellSize.x, (int)cellSize.y);

        cellViews = new InventoryCellView[width, height];

        for (int y = 0; y < height; y++)
            for (int x = 0; x < width; x++)
            {
                var view = inventoryFactory.CreateInventoryCell(grid.transform);
                cellViews[x, y] = view;
            }
    }

    private void UpdateItemViews(IReadOnlyDictionary<InventoryItemId, InventoryItemViewData> items)
    {
        DestroyAllItemViews();
        CreateNewItemViews(items);
    }

    private void DestroyAllItemViews()
    {
        foreach (var itemView in itemViews)
            Destroy(itemView.gameObject);
        itemViews.Clear();
    }

    private void CreateNewItemViews(IReadOnlyDictionary<InventoryItemId, InventoryItemViewData> items)
    {
        foreach (var kv in items)
        {
            var itemView = inventoryFactory.CreateInventoryItemView(kv.Value.Item,
              itemsRoot,
              grid.cellSize,
              canvas.scaleFactor);

            itemViews.Add(itemView);
        }
    }

    private void OnDestroy()
    {
        if (presenter != null)
        {
            presenter.OnBuildGridBackground -= BuildGrid;
            presenter.OnUpdateInventoryViewOld -= UpdateItemViews;
        }
    }
}