using System;
using System.Collections.Generic;
using Zenject;

public class CraftReturnItemSystem : IInitializable, IDisposable
{
    private readonly ICraftBus craftBus;
    private readonly ICraftSlotService craftSlotService;
    private readonly IInventoryBus bus;

    public CraftReturnItemSystem(
       ICraftBus _craftBus,
       ICraftSlotService _craftSlotService,
       IInventoryBus _bus)
    {
        craftBus = _craftBus;
        craftSlotService = _craftSlotService;
        bus = _bus;
    }

    public void Initialize()
    {
        craftBus.OnCraftReturnItems += OnCancelCrafting;
    }

    private void OnCancelCrafting(List<CraftingSlot> slots)
    {
        foreach (var slot in craftSlotService.GetCraftSlotList())
            bus.AddItem(slot.ItemId, slot.CurrentCount);

        craftSlotService.CleanupSlots();
    }

    public void Dispose()
    {
        craftBus.OnCraftReturnItems -= OnCancelCrafting;
    }
}

