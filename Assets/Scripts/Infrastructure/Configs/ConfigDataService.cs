using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public interface IConfigDataService
{
    void Load();
    UpgradeCardConfig GetUpgradeCardConfig(CardUpgradeId id);
    UpgradeCardConfig GetRandomCard();
    ItemConfig GetItemConfig(ItemId id);
    ItemConfig GetRandomItem();
    CraftRecipeConfig GetRecipe(RecipeId id);
    ChestConfig GetChest(string id);
    IReadOnlyDictionary<string, ChestConfig> GetAllChests();
    IReadOnlyDictionary<RecipeId, CraftRecipeConfig> GetAllRecipes();
    public InventoryConfig GetInventoryConfig();

}

public class ConfigDataService : IConfigDataService
{
    private Dictionary<CardUpgradeId, UpgradeCardConfig> cards = new();
    private Dictionary<ItemId, ItemConfig> items = new();
    private Dictionary<RecipeId, CraftRecipeConfig> recipes = new();
    private Dictionary<string, ChestConfig> chests = new();

    private InventoryConfig inventoryConfig;

    public void Load()
    {
        cards = Resources.LoadAll<UpgradeCardConfig>("Configs/Cards")
            .ToDictionary(x => x.CardId, x => x);

        items = Resources.LoadAll<ItemConfig>("Configs/Items")
            .ToDictionary(x => x.ItemId, x => x);

        inventoryConfig = Resources.Load<InventoryConfig>("Configs/InventoryConfig");

        recipes = Resources
         .LoadAll<CraftRecipeConfig>("Configs/Recipes")
         .ToDictionary(x => x.Id, x => x);

        chests = Resources
         .LoadAll<ChestConfig>("Configs/Chests")
         .ToDictionary(x => x.Id, x => x);
    }

    public UpgradeCardConfig GetUpgradeCardConfig(CardUpgradeId id) =>
     cards.TryGetValue(id, out UpgradeCardConfig config)
        ? config
        : null;

    public ItemConfig GetItemConfig(ItemId id) =>
    items.TryGetValue(id, out ItemConfig config)
       ? config
       : null;

    public ChestConfig GetChest(string id) =>
    chests.TryGetValue(id, out ChestConfig config)
       ? config
       : null;

    public UpgradeCardConfig GetRandomCard()
    {
        int index = Random.Range(0, cards.Count);
        UpgradeCardConfig randomElement = cards.ElementAt(index).Value;
        return randomElement;
    }

    public ItemConfig GetRandomItem()
    {
        int index = Random.Range(0, items.Count);
        ItemConfig randomElement = items.ElementAt(index).Value;
        return randomElement;
    }
    public CraftRecipeConfig GetRecipe(RecipeId id) =>
     recipes.TryGetValue(id, out var config)
        ? config
        : null;

    public IReadOnlyDictionary<RecipeId, CraftRecipeConfig> GetAllRecipes() => recipes;
    public IReadOnlyDictionary<string, ChestConfig> GetAllChests() => chests;

    public InventoryConfig GetInventoryConfig() => inventoryConfig;
}
