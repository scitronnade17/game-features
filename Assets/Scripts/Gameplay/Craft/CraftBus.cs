using System;
using System.Collections.Generic;

public interface ICraftBus
{
    event Action<InventoryItemId, ItemId> OnTryPutItemToCraftSlotDrop;
    event Action OnCraftClicked;
    event Action<RecipeId> OnReceiptButtonClick;
    event Action OnHideCraftPanel;
    void ReceiptButtonClick(RecipeId recipeId);
    void CraftClicked();
    void TryPutItemToCraftSlotClick(InventoryItemId inventoryItemId, ItemId slotKey);
    void HideCraftPanel();

    event Action<List<CraftingSlot>> OnCraftReturnItems;
    void CraftReturnItems(List<CraftingSlot> slots);
}

public class CraftBus : ICraftBus
{
    public event Action<InventoryItemId, ItemId> OnTryPutItemToCraftSlotDrop;
    public event Action OnCraftClicked;
    public event Action<RecipeId> OnReceiptButtonClick;
    public event Action OnHideCraftPanel;

    public void ReceiptButtonClick(RecipeId recipeId) =>
       OnReceiptButtonClick?.Invoke(recipeId);

    public void CraftClicked() =>
       OnCraftClicked?.Invoke();

    public void TryPutItemToCraftSlotClick(InventoryItemId inventoryItemId, ItemId slotKey) =>
       OnTryPutItemToCraftSlotDrop?.Invoke(inventoryItemId, slotKey);

    public void HideCraftPanel() =>
       OnHideCraftPanel?.Invoke();

    public event Action<List<CraftingSlot>> OnCraftReturnItems;

    public void CraftReturnItems(List<CraftingSlot> slots) =>
       OnCraftReturnItems?.Invoke(slots);

}