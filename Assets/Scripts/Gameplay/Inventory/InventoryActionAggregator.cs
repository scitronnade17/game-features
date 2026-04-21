using System;
using UnityEngine;
using UnityEngine.EventSystems;

public interface IInventoryActionAggregator
{
    event Action<InventoryItemId> OnBeginDrag;
    event Action<InventoryItemId, Vector2, PointerEventData> OnDrag;
    event Action<InventoryItemId, Vector2, PointerEventData> OnEndDrag;
    event Action<InventoryItemId> OnRightClicked;
    void ItemBeginDrag(InventoryItemId id);
    void ItemEndDrag(InventoryItemId inventoryItemView, Vector2 delta, PointerEventData eventData);
    void ItemDrag(InventoryItemId inventoryItemView, Vector2 delta, PointerEventData eventData);
    void InventoryItemRightClick(InventoryItemId inventoryItemView);
}

public class InventoryActionAggregator : IInventoryActionAggregator
{
    public event Action<InventoryItemId> OnBeginDrag;
    public event Action<InventoryItemId, Vector2, PointerEventData> OnDrag;
    public event Action<InventoryItemId, Vector2, PointerEventData> OnEndDrag;
    public event Action<InventoryItemId> OnRightClicked;

    public void ItemBeginDrag(InventoryItemId id) =>
      OnBeginDrag?.Invoke(id);

    public void ItemEndDrag(InventoryItemId inventoryItemView, Vector2 delta, PointerEventData eventData) =>
      OnEndDrag?.Invoke(inventoryItemView, delta, eventData);

    public void ItemDrag(InventoryItemId inventoryItemView, Vector2 delta, PointerEventData eventData) =>
      OnDrag?.Invoke(inventoryItemView, delta, eventData);

    public void InventoryItemRightClick(InventoryItemId inventoryItemView) =>
      OnRightClicked?.Invoke(inventoryItemView);
}