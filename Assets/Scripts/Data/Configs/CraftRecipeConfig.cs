using System;
using UnityEngine;

[CreateAssetMenu(fileName = "CraftRecipe", menuName = "Configs/CraftRecipe")]
public class CraftRecipeConfig : ScriptableObject
{
    public string Name;
    public RecipeId Id;
    public Sprite Icon;

    public ItemConfig ResultItemConfig;
    public int ResultCount = 1;

    [Serializable]
    public struct IngredientConfig
    {
        public ItemConfig Item;
        public int Count;
    }

    public IngredientConfig[] Ingredients;

    public CraftingRecipe ToRuntime()
    {
        var list = new CraftingIngredient[Ingredients.Length];
        for (int i = 0; i < Ingredients.Length; i++)
            list[i] = new CraftingIngredient(Ingredients[i].Item.ItemId, Ingredients[i].Count);

        return new CraftingRecipe(
          id: Id,
          ingredients: list,
          resultId: ResultItemConfig.ItemId,
          resultCount: ResultCount
        );
    }
}
