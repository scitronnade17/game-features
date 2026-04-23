using UnityEngine;
using UnityEngine.UI;
using Zenject;

public sealed class CraftingSlotView : MonoBehaviour, ICraftingDropTarget
{
    public InventoryDropTargetType TargetType { get; } = InventoryDropTargetType.Craft;

    [SerializeField] private Image icon;
    [SerializeField] private Text countText;

    private ItemId itemId;
    private ICraftBus craftBus;

    public ItemId ItemId => itemId;

    public void Setup(ItemId _itemId, ItemConfig config, ICraftBus bus)
    {
        itemId = _itemId;
        icon.sprite = config.Icon;
        icon.enabled = icon.sprite != null;
        craftBus = bus;
    }

    public void TryPut(InventoryItemId inventoryItemId)
    {
        craftBus.TryPutItemToCraftSlotClick(inventoryItemId, itemId);
    }

    public void UpdateView(ItemConfig config, CraftingSlot slot)
    {
        icon.sprite = config.Icon;
        icon.enabled = icon.sprite != null;
        countText.text = $"{slot.CurrentCount} / {slot.MaxCount}";
    }

    public void ClearView()
    {
        icon.enabled = false;
        countText.text = string.Empty;
    }

}

