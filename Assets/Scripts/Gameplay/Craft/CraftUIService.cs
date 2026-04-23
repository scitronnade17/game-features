using System.Collections.Generic;
using UnityEngine;

public interface ICraftUIService
{
    void BuildReceiptsList(Transform parent);
    void RebuildCraftSlotViews(IReadOnlyDictionary<ItemId, CraftingSlot> slots, Transform parent);
    void UpdateCraftSlotView(ItemId itemId, ItemConfig config, CraftingSlot slot);
    void ClearCraftSlotViews();
}

public class CraftUIService : ICraftUIService
{
    private readonly IConfigDataService configs;
    private readonly ICraftFactory craftFactory;

    private readonly Dictionary<ItemId, CraftingSlotView> slotViews = new();

    public CraftUIService(
       IConfigDataService _configs,
       ICraftFactory _craftFactory)
    {
        configs = _configs;
        craftFactory = _craftFactory;
    }

    public void BuildReceiptsList(Transform parent)
    {
        foreach (Transform child in parent)
            Object.Destroy(child.gameObject);

        foreach (CraftRecipeConfig config in configs.GetAllRecipes().Values)
            craftFactory.CreateReceiptView(config, parent);
    }

    public void RebuildCraftSlotViews(IReadOnlyDictionary<ItemId, CraftingSlot> slots, Transform parent)
    {
        ClearCraftSlotViews();

        foreach (var kv in slots)
        {
            CraftingSlot slot = kv.Value;
            CraftingIngredient ingredient = new CraftingIngredient(slot.ItemId, slot.MaxCount);

            CraftingSlotView slotView = craftFactory.CreateCraftIngridientSlot(ingredient, parent);
            slotViews.Add(slot.ItemId, slotView);
        }
    }

    public void UpdateCraftSlotView(ItemId itemId, ItemConfig config, CraftingSlot slot)
    {
        if (!slotViews.TryGetValue(itemId, out CraftingSlotView slotView))
            return;

        slotView.UpdateView(config, slot);
    }

    public void ClearCraftSlotViews()
    {
        foreach (CraftingSlotView slotView in slotViews.Values)
        {
            if (slotView != null)
                Object.Destroy(slotView.gameObject);
        }

        slotViews.Clear();
    }
}
