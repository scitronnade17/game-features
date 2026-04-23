using System.Collections.Generic;

public class CraftingRecipe
{
    public RecipeId Id { get; }
    public IReadOnlyList<CraftingIngredient> Ingredients { get; }
    public ItemId ResultId { get; }
    public int ResultCount { get; }

    public CraftingRecipe(
      RecipeId id,
      IReadOnlyList<CraftingIngredient> ingredients,
      ItemId resultId,
      int resultCount = 1)
    {
        Id = id;
        Ingredients = ingredients;
        ResultId = resultId;
        ResultCount = resultCount;
    }
}
