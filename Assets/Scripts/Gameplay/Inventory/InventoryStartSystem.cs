using Zenject;

public class InventoryStartSystem : IInitializable
{
    private readonly IInventoryPanelService inventoryPanel;
    private readonly IInventoryPanelPresenter inventoryPanelPresenter;

    public InventoryStartSystem(IInventoryPanelService _inventoryPanel,
       IInventoryPanelPresenter _inventoryPanelPresenter)
    {
        inventoryPanel = _inventoryPanel;
        inventoryPanelPresenter = _inventoryPanelPresenter;
    }

    public void Initialize()
    {
        inventoryPanel.BuildGridBackground();
        inventoryPanelPresenter.HideInventoryWindow();
    }
}