using UnityEngine;
using Zenject;

public class InventoryWindowSystem : ITickable
{
    private readonly IInventoryPanelPresenter inventoryPanelPresenter;
    private readonly ICraftUIService craftUIService;

    public InventoryWindowSystem(IInventoryPanelPresenter _inventoryPanelPresenter,
        ICraftUIService _craftUIService)
    {
        inventoryPanelPresenter = _inventoryPanelPresenter;
        craftUIService = _craftUIService;
    }

    public void Tick()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryPanelPresenter.ShowInventoryWindow();
            craftUIService.ClearCraftSlotViews();
        }
    }
}