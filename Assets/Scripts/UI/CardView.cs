using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class CardView: MonoBehaviour
{
    [SerializeField] private Text label;
    [SerializeField] private Image icon;
    [SerializeField] private Button button;

    public CardUpgradeId CardUpgadeId;

    private IEventBus eventBus;

    [Inject]
    public void Construct(IEventBus _eventBus)
    {
        eventBus = _eventBus;
    }

    private void Start()
    {
        button.onClick.AddListener(Click);
    }

    public void SetupCard(Sprite _icon, string _label)
    {
        icon.sprite = _icon;
        label.text = _label;
    }

    private void Click()
    {
        eventBus.RaiseEvent(new UpgradeSignal(CardUpgadeId));
    }

    public void DestroyCard() =>
      Destroy(gameObject);
}