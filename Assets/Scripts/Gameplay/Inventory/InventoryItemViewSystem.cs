using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class InventoryItemViewSystem : IInitializable, IDisposable
{
    private readonly IInventoryActionAggregator eventAggregator;
    private readonly IInventoryPanelService inventoryPanel;
    private readonly IInventoryService inventory;

    public InventoryItemViewSystem(IInventoryActionAggregator _eventAggregator,
       IInventoryPanelService _inventoryPanel,
       IInventoryService _inventory)
    {
        eventAggregator = _eventAggregator;
        inventoryPanel = _inventoryPanel;
        inventory = _inventory;
    }

    public void Initialize()
    {
        eventAggregator.OnBeginDrag += Begin;
        eventAggregator.OnDrag += Drag;
        eventAggregator.OnEndDrag += EndDrag;
    }

    private void Begin(InventoryItemId itemId)
    {
    }

    private void Drag(InventoryItemId itemId, Vector2 pos, PointerEventData eventData)
    {
    }

    private void EndDrag(InventoryItemId itemId, Vector2 gridPosition, PointerEventData eventData)
    {
        Vector2 pos = gridPosition;

        int newX = Mathf.RoundToInt(pos.x / inventory.CellSize.x);
        int newY = Mathf.RoundToInt(-pos.y / inventory.CellSize.y);

        inventory.TryMoveItem(itemId, newX, newY);
    }

    public void Dispose()
    {
        eventAggregator.OnBeginDrag -= Begin;
        eventAggregator.OnDrag -= Drag;
        eventAggregator.OnEndDrag -= EndDrag;
    }
}