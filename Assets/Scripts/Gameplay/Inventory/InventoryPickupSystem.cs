using System;
using UnityEngine;
using Zenject;

public class InventoryPickupSystem : IInitializable, IDisposable
{
    private readonly IEventBus eventBus;
    private readonly IInventoryService inventoryService;

    public InventoryPickupSystem(IEventBus _eventBus,
       IInventoryService _inventoryService)
    {
        eventBus = _eventBus;
        inventoryService = _inventoryService;
    }

    public void Initialize()
    {
        eventBus.Subscribe<TryPickupItemSignal>(TryInventoryPickup);
    }

    private void TryInventoryPickup(TryPickupItemSignal signal)
    {

        if (inventoryService.TryAddNewItem(signal.ItemId, signal.Count, out var item).IsSuccess)
        {
            GameObject.Destroy(signal.ItemWorld.gameObject);
        }
    }

    public void Dispose()
    {
        eventBus.Unsubscribe<TryPickupItemSignal>(TryInventoryPickup);
    }
}