using UnityEngine;
using Zenject;

public class InventoryWindow : MonoBehaviour
{
    private IInventoryPanelPresenter presenter;

    [Inject]
    public void Construct(IInventoryPanelPresenter _presenter)
    {
        presenter = _presenter;
        presenter.OnShowInventoryCraftWindow += Show;
        presenter.OnHideInventoryCraftWindow += Hide;
    }

    private void Show()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        if (presenter != null)
        {
            presenter.OnShowInventoryCraftWindow -= Show;
            presenter.OnHideInventoryCraftWindow -= Hide;
        }
    }
}