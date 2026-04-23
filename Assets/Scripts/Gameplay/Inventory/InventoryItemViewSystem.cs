using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class InventoryItemViewSystem : IInitializable, IDisposable
{
    private readonly IInventoryBus bus;
    private readonly IInventoryPanelService inventoryPanel;
    private readonly IInventoryService inventory;
    private readonly IInventoryDropTargetResolver dropTargetResolver;

    public InventoryItemViewSystem(IInventoryBus _bus,
       IInventoryPanelService _inventoryPanel,
       IInventoryService _inventory,
       IInventoryDropTargetResolver _dropTargetResolver)
    {
        bus = _bus;
        inventoryPanel = _inventoryPanel;
        inventory = _inventory;
        dropTargetResolver = _dropTargetResolver;
    }

    public void Initialize()
    {
        bus.OnBeginDrag += Begin;
        bus.OnDrag += Drag;
        bus.OnEndDrag += EndDrag;
    }

    private void Begin(InventoryItemId itemId)
    {
    }

    private void Drag(InventoryItemId itemId, Vector2 pos, PointerEventData eventData)
    {
    }

    private void EndDrag(InventoryItemId itemId, Vector2 gridPosition, PointerEventData eventData)
    {
        InventoryDropTargetResult dropResult = dropTargetResolver.Resolve(eventData);

        switch (dropResult.Type)
        {
            case InventoryDropTargetType.Craft:
                {
                    ICraftingDropTarget craftTarget = (ICraftingDropTarget)dropResult.Target;
                    craftTarget.TryPut(itemId);
                    inventoryPanel.UpdateItemViews();
                    return;
                }

            case InventoryDropTargetType.Inventory:
                {
                    inventory.TryMoveItem(itemId, gridPosition);
                    return;
                }

            case InventoryDropTargetType.None:
            default:
                {
                    break;
                }
        }
    }

    public void Dispose()
    {
        bus.OnBeginDrag -= Begin;
        bus.OnDrag -= Drag;
        bus.OnEndDrag -= EndDrag;
    }
}