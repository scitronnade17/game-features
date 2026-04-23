using System;
using Zenject;

public class TryPutItemToCraftSlotSystem : IInitializable, IDisposable
{
    private readonly ICraftSlotService craftSlotService;
    private readonly ICraftBus craftBus;
    private readonly IInventoryBus inventoryBus;
    private readonly IInventoryService inventory;

    public TryPutItemToCraftSlotSystem(ICraftSlotService _craftSlotService,
       ICraftBus _craftBus,
       IInventoryBus _inventoryBus,
       IInventoryService _inventory)
    {
        craftSlotService = _craftSlotService;
        craftBus = _craftBus;
        inventoryBus = _inventoryBus;
        inventory = _inventory;
    }

    public void Initialize()
    {
        craftBus.OnTryPutItemToCraftSlotDrop += TryPut;
    }

    private void TryPut(InventoryItemId inventoryItemId, ItemId itemId)
    {
        var item = inventory.GetItemById(inventoryItemId);

        if (craftSlotService.TryPutInSlot(item, itemId, out int removedCount).IsSuccess)
        {
            inventoryBus.RemoveItem(inventoryItemId, removedCount);
        }
    }
    public void Dispose()
    {
        craftBus.OnTryPutItemToCraftSlotDrop -= TryPut;
    }
}