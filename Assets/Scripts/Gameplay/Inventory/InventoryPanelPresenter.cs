using System;
using System.Collections.Generic;
using UnityEngine;

public interface IInventoryPanelPresenter
{
    event Action<InventoryCellViewData[,], int, int, Vector2> OnBuildGridBackground;
    event Action<IReadOnlyDictionary<InventoryItemId, InventoryItemViewData>> OnUpdateInventoryViewOld;
    event Action OnShowInventoryCraftWindow;
    event Action OnHideInventoryCraftWindow;
    void ShowInventoryWindow();
    void HideInventoryWindow();
}

public class InventoryPanelPresenter : IInventoryPanelPresenter, IDisposable
{
    public event Action<InventoryCellViewData[,], int, int, Vector2> OnBuildGridBackground;
    public event Action<IReadOnlyDictionary<InventoryItemId, InventoryItemViewData>> OnUpdateInventoryViewOld;
    public event Action OnShowInventoryCraftWindow;
    public event Action OnHideInventoryCraftWindow;

    private readonly IInventoryPanelService inventoryPanel;

    public InventoryPanelPresenter(
      IInventoryPanelService _inventoryPanel)
    {
        inventoryPanel = _inventoryPanel;

        inventoryPanel.OnBuildGridBackground += BuildGrid;
        inventoryPanel.OnUpdateItemViews += UpdateInventoryView;
    }

    private void BuildGrid(InventoryCellViewData[,] cellsData, int width, int height, Vector2 cellSize) =>
      OnBuildGridBackground?.Invoke(cellsData, width, height, cellSize);

    private void UpdateInventoryView(IReadOnlyDictionary<InventoryItemId, InventoryItemViewData> viewsData) =>
      OnUpdateInventoryViewOld?.Invoke(viewsData);

    public void ShowInventoryWindow() =>
      OnShowInventoryCraftWindow?.Invoke();

    public void HideInventoryWindow() =>
      OnHideInventoryCraftWindow?.Invoke();

    public void Dispose()
    {
        inventoryPanel.OnBuildGridBackground -= BuildGrid;
        inventoryPanel.OnUpdateItemViews -= UpdateInventoryView;
    }
}