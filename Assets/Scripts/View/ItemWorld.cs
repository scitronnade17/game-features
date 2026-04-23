using UnityEngine;
using Zenject;

public class ItemWorld : MonoBehaviour
{
    [field: SerializeField] public int Count { get; private set; }
    private ItemId itemId;
    private IInventoryBus eventBus;
    private bool isPickup;

    [Inject]
    public void Construct(IInventoryBus _eventBus)
    {
        eventBus = _eventBus;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isPickup) return;

        isPickup = true;
        eventBus.TryInventoryPickupFromWorld(itemId, this.gameObject, Count);
    }

    public void SetKey(ItemId _itemId) =>
      itemId = _itemId;
}