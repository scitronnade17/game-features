using System.Linq;
using Zenject;

public class CraftPanelUISystem : IInitializable
{
    private readonly ICraftService craftService;
    private readonly ICraftPanelPresenter craftPanelPresenter;
    private readonly ICraftBus craftBus;

    public CraftPanelUISystem(
      ICraftService _craftService,
      ICraftPanelPresenter _craftPanelPresenter,
      ICraftBus _craftBus)
    {
        craftService = _craftService;
        craftPanelPresenter = _craftPanelPresenter;
        craftBus = _craftBus;
    }

    public void Initialize()
    {
        craftBus.OnHideCraftPanel += HideCraftPanel;
        craftPanelPresenter.BuildReceiptsList();
    }

    private void HideCraftPanel()
    {
        craftBus.CraftReturnItems(craftService.Slots.Values.ToList());
    }
}
