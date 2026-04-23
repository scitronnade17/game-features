using System;
using UnityEngine;
using Zenject;
public class InventoryPickupSystem : IInitializable, IDisposable
{
    private readonly IInventoryBus eventBus;
    private readonly IInventoryService inventoryService;

    public InventoryPickupSystem(IInventoryBus _eventBus,
       IInventoryService _inventoryService)
    {
        eventBus = _eventBus;
        inventoryService = _inventoryService;
    }

    public void Initialize()
    {
        eventBus.OnTryInventoryPickupFromWorld += TryInventoryPickup;
        eventBus.OnAddInventoryItem += AddItem;
        eventBus.OnRemoveInventoryItem += RemoveItem;
    }

    private void TryInventoryPickup(ItemId itemId, GameObject itemWorld, int count)
    {
        if (inventoryService.TryAddNewItem(itemId, count, out var item).IsSuccess)
        {
            GameObject.Destroy(itemWorld.gameObject);
        }
    }

    private void AddItem(ItemId itemId, int count)
    {
        inventoryService.TryAddNewItem(itemId, count, out var item);
    }

    private void RemoveItem(InventoryItemId itemId, int count)
    {
        var item = inventoryService.GetItemById(itemId);
        item.RemoveFromStack(count);

        if (item.Count <= 0)
            inventoryService.TryRemoveItem(item.InventoryId, out _);
    }

    public void Dispose()
    {
        eventBus.OnTryInventoryPickupFromWorld -= TryInventoryPickup;
        eventBus.OnAddInventoryItem -= AddItem;
        eventBus.OnRemoveInventoryItem -= RemoveItem;
    }
}