using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ItemCreateButton : MonoBehaviour
{
    [SerializeField] private Button itemButton;

    private IEventBus eventBus;

    [Inject]
    public void Construct(IEventBus _eventBus)
    {
        eventBus = _eventBus;
    }

    public void Start()
    {
        itemButton.onClick.AddListener(Click);
    }

    private void Click()
    {
        eventBus.RaiseEvent(new CreateItemSignal());
    }
}