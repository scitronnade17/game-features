using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public interface ICraftService
{
    event Action<CraftRecipeConfig> OnRecipeSelected;
    event Action OnSlotsRebuilt;
    event Action OnSlotsCleared;
    event Action OnCraftCompleted;
    CraftRecipeConfig SelectedConfig { get; }
    CraftingRecipe SelectedRecipe { get; }
    IReadOnlyDictionary<ItemId, CraftingSlot> Slots { get; }
    void SelectRecipe(CraftRecipeConfig config);
    void ClearSlots();
    CraftResult CanCraft();
    CraftResult TryCraft(out CraftingRecipe craftedRecipe);
}

public class CraftService : ICraftService
{
    public event Action<CraftRecipeConfig> OnRecipeSelected;
    public event Action OnSlotsRebuilt;
    public event Action OnSlotsCleared;
    public event Action OnCraftCompleted;

    public CraftRecipeConfig SelectedConfig { get; private set; }
    public CraftingRecipe SelectedRecipe { get; private set; }

    private readonly Dictionary<ItemId, CraftingSlot> slots = new();
    public IReadOnlyDictionary<ItemId, CraftingSlot> Slots => slots;

    public void SelectRecipe(CraftRecipeConfig config)
    {
        SelectedConfig = config;
        SelectedRecipe = config?.ToRuntime();

        RebuildSlots();

        OnRecipeSelected?.Invoke(config);
        OnSlotsRebuilt?.Invoke();
    }

    public void ClearSlots()
    {
        foreach (CraftingSlot slot in slots.Values)
            slot.Clear();

        slots.Clear();
        OnSlotsCleared?.Invoke();
    }

    public CraftResult CanCraft()
    {
        return CanCraftInternal(SelectedRecipe, slots.Values);
    }

    public CraftResult TryCraft(out CraftingRecipe craftedRecipe)
    {
        craftedRecipe = null;

        CraftResult canCraftResult = CanCraftInternal(SelectedRecipe, slots.Values);
        if (canCraftResult.IsFail)
            return canCraftResult;

        List<CraftingSlot> slotsList = slots.Values.ToList();

        if (!TryConsumeFromSlots(slotsList, SelectedRecipe.Ingredients))
        {
            return CraftResult.Fail(CraftFailReason.ConsumeFailed);
        }

        craftedRecipe = SelectedRecipe;

        OnCraftCompleted?.Invoke();
        return CraftResult.Success();
    }

    private void RebuildSlots()
    {
        slots.Clear();

        if (SelectedRecipe == null)
            return;

        foreach (CraftingIngredient ingredient in SelectedRecipe.Ingredients)
        {
            if (slots.ContainsKey(ingredient.ItemId))
            {
                continue;
            }

            slots.Add(ingredient.ItemId, new CraftingSlot(ingredient.ItemId, ingredient.Count));
        }
    }

    private CraftResult CanCraftInternal(CraftingRecipe recipe, IEnumerable<CraftingSlot> slots)
    {
        if (recipe == null)
            return CraftResult.Fail(CraftFailReason.NoSelectedRecipe);

        if (recipe.Ingredients == null)
            return CraftResult.Fail(CraftFailReason.InvalidRecipe);

        if (slots == null)
            return CraftResult.Fail(CraftFailReason.InvalidRecipe);

        Dictionary<ItemId, int> available = CountItemsInSlots(slots);

        foreach (CraftingIngredient ingredient in recipe.Ingredients)
        {
            if (!available.TryGetValue(ingredient.ItemId, out int have) || have < ingredient.Count)
                return CraftResult.Fail(CraftFailReason.NotEnoughIngredients);
        }

        return CraftResult.Success();
    }

    private Dictionary<ItemId, int> CountItemsInSlots(IEnumerable<CraftingSlot> slots)
    {
        Dictionary<ItemId, int> result = new();

        foreach (CraftingSlot slot in slots)
        {
            if (slot == null)
                continue;

            if (slot.CurrentCount <= 0)
                continue;

            if (!result.TryGetValue(slot.ItemId, out int count))
                count = 0;

            count += slot.CurrentCount;
            result[slot.ItemId] = count;
        }

        return result;
    }

    private bool TryConsumeFromSlots(IReadOnlyList<CraftingSlot> slots, IEnumerable<CraftingIngredient> ingredients)
    {
        foreach (CraftingIngredient ingredient in ingredients)
        {
            int need = ingredient.Count;

            foreach (CraftingSlot slot in slots)
            {
                if (!Equals(slot.ItemId, ingredient.ItemId))
                    continue;

                if (need <= 0)
                    break;

                int canTake = Mathf.Min(slot.CurrentCount, need);
                if (canTake <= 0)
                    continue;

                slot.Remove(canTake);
                need -= canTake;
            }

            if (need > 0)
                return false;
        }

        return true;
    }
}
