using UnityEngine;
using Zenject;

public class InventoryWindowSystem : ITickable
{
    private readonly IInventoryPanelPresenter inventoryPanelPresenter;

    public InventoryWindowSystem(IInventoryPanelPresenter _inventoryPanelPresenter)
    {
        inventoryPanelPresenter = _inventoryPanelPresenter;
    }

    public void Tick()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryPanelPresenter.ShowInventoryWindow();
        }
    }
}