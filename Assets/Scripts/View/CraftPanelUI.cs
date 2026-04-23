using UnityEngine;
using UnityEngine.UI;
using Zenject;

public sealed class CraftPanelUI : MonoBehaviour
{
    [field: SerializeField] public Transform IngridientRoot { get; private set; }
    [field: SerializeField] public Transform ReceiptRoot { get; private set; }
    [SerializeField] private Text resultName;
    [SerializeField] private Button craftButton;
    [SerializeField] private Image resultIcon;

    private ICraftBus craftBus;

    [Inject]
    public void Construct(
      ICraftPanelPresenter presenter,
      ICraftBus _craftBus)
    {
        craftBus = _craftBus;

        presenter.Register(this);

        craftButton.onClick.AddListener(OnCraftClicked);
    }

    public void UpdateRecipeHeader(CraftRecipeConfig config)
    {
        if (resultIcon != null)
        {
            resultIcon.sprite = config.Icon;
            resultIcon.enabled = config.Icon != null;
        }

        if (resultName != null)
            resultName.text = config.Name;
    }

    private void OnDisable()
    {
        resultName.text = "Select Recipe";
        resultIcon.enabled = false;
        craftBus.HideCraftPanel();
    }

    private void OnCraftClicked() =>
      craftBus.CraftClicked();
}
