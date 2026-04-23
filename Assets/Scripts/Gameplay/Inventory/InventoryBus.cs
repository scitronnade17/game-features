using System;
using UnityEngine;
using UnityEngine.EventSystems;

public interface IInventoryBus
{
    event Action<InventoryItemId> OnBeginDrag;
    event Action<InventoryItemId, Vector2, PointerEventData> OnDrag;
    event Action<InventoryItemId, Vector2, PointerEventData> OnEndDrag;
    event Action<InventoryItemId> OnRightClicked;
    event Action<ItemId, int> OnAddInventoryItem;
    event Action<InventoryItemId, int> OnRemoveInventoryItem;
    event Action<ItemId, GameObject, int> OnTryInventoryPickupFromWorld;
    void AddItem(ItemId itemId, int count);
    void RemoveItem(InventoryItemId itemId, int count);
    void ItemBeginDrag(InventoryItemId id);
    void ItemEndDrag(InventoryItemId inventoryItemView, Vector2 delta, PointerEventData eventData);
    void ItemDrag(InventoryItemId inventoryItemView, Vector2 delta, PointerEventData eventData);
    void InventoryItemRightClick(InventoryItemId inventoryItemView);
    void TryInventoryPickupFromWorld(ItemId itemId, GameObject itemWorld, int count);
}

public class InventoryBus : IInventoryBus
{
    public event Action<InventoryItemId> OnBeginDrag;
    public event Action<InventoryItemId, Vector2, PointerEventData> OnDrag;
    public event Action<InventoryItemId, Vector2, PointerEventData> OnEndDrag;
    public event Action<InventoryItemId> OnRightClicked;
    public event Action<ItemId, GameObject, int> OnTryInventoryPickupFromWorld;

    public void ItemBeginDrag(InventoryItemId id) =>
      OnBeginDrag?.Invoke(id);

    public void ItemEndDrag(InventoryItemId inventoryItemView, Vector2 delta, PointerEventData eventData) =>
      OnEndDrag?.Invoke(inventoryItemView, delta, eventData);

    public void ItemDrag(InventoryItemId inventoryItemView, Vector2 delta, PointerEventData eventData) =>
      OnDrag?.Invoke(inventoryItemView, delta, eventData);

    public void InventoryItemRightClick(InventoryItemId inventoryItemView) =>
      OnRightClicked?.Invoke(inventoryItemView);

    public event Action<ItemId, int> OnAddInventoryItem;
    public event Action<InventoryItemId, int> OnRemoveInventoryItem;

    public void AddItem(ItemId itemId, int count) =>
       OnAddInventoryItem?.Invoke(itemId, count);

    public void RemoveItem(InventoryItemId itemId, int count) =>
       OnRemoveInventoryItem?.Invoke(itemId, count);

    public void TryInventoryPickupFromWorld(ItemId itemId, GameObject itemWorld, int count) =>
   OnTryInventoryPickupFromWorld?.Invoke(itemId, itemWorld, count);
}