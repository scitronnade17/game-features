using UnityEngine;

public interface ICraftFactory
{
    CraftingSlotView CreateCraftIngridientSlot(CraftingIngredient ingredient, Transform parent);
    CraftRecipeButtonView CreateReceiptView(CraftRecipeConfig config, Transform parent);
}

public class CraftFactory : ICraftFactory
{
    private readonly IConfigDataService configs;
    private readonly ICraftBus bus;

    public CraftFactory(IConfigDataService _configs, ICraftBus bus)
    {
        configs = _configs;
        this.bus = bus;
    }

    public CraftingSlotView CreateCraftIngridientSlot(CraftingIngredient ingredient, Transform parent)
    {
        var gameConfig = configs.GetInventoryConfig();
        var slotObject = Object.Instantiate(gameConfig.IngridientSlotPrefab, parent);
        var slot = slotObject.GetComponent<CraftingSlotView>();
        slot.Setup(ingredient.ItemId, configs.GetItemConfig(ingredient.ItemId), bus);
        return slot;
    }


    public CraftRecipeButtonView CreateReceiptView(CraftRecipeConfig config, Transform parent)
    {
        var gameConfig = configs.GetInventoryConfig();
        var reciptObject = Object.Instantiate(gameConfig.CraftRecipeViewPrefab, parent);
        var recipe = reciptObject.GetComponent<CraftRecipeButtonView>();
        recipe.Setup(config, config.Id, bus);
        return recipe;
    }
}
