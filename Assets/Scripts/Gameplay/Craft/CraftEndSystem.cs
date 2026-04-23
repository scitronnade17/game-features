using System;
using Zenject;

public class CraftEndSystem : IInitializable, IDisposable
{
    private readonly IInventoryBus bus;
    private readonly ICraftService craftService;
    private readonly ICraftBus craftBus;

    public CraftEndSystem(ICraftBus _craftBus,
      ICraftService _craftService,
      IInventoryBus _bus)
    {
        craftBus = _craftBus;
        craftService = _craftService;
        bus = _bus;
    }

    public void Initialize()
    {
        craftBus.OnCraftClicked += Craft;
    }

    private void Craft()
    {
        if (craftService.TryCraft(out var craftItemRecipe).IsSuccess)
        {
            bus.AddItem(craftItemRecipe.ResultId, craftItemRecipe.ResultCount);
        }
    }

    public void Dispose()
    {
        craftBus.OnCraftClicked -= Craft;
    }
}
