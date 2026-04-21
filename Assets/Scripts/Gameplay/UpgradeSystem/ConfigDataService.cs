using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public interface IConfigDataService
{
    void Load();
    LevelUpCardConfig GetLevelUpCardConfig(CardUpgradeId id);
    LevelUpCardConfig GetRandomCard();
    ItemConfig GetItemConfig(ItemId id);
    ItemConfig GetRandomItem();
    public InventoryConfig GetInventoryConfig();

}

public class ConfigDataService : IConfigDataService
{
    private Dictionary<CardUpgradeId, LevelUpCardConfig> cards = new();
    private Dictionary<ItemId, ItemConfig> items = new();

    private InventoryConfig inventoryConfig;

    public void Load()
    {
        cards = Resources.LoadAll<LevelUpCardConfig>("Configs/Cards")
            .ToDictionary(x => x.CardId, x => x);
        items = Resources.LoadAll<ItemConfig>("Configs/Items")
            .ToDictionary(x => x.ItemId, x => x);
        inventoryConfig = Resources.Load<InventoryConfig>("Configs/InventoryConfig");
        Debug.Log(inventoryConfig);
    }

    public LevelUpCardConfig GetLevelUpCardConfig(CardUpgradeId id) =>
     cards.TryGetValue(id, out LevelUpCardConfig config)
        ? config
        : null;

    public ItemConfig GetItemConfig(ItemId id) =>
    items.TryGetValue(id, out ItemConfig config)
       ? config
       : null;

    public LevelUpCardConfig GetRandomCard()
    {
        int index = Random.Range(0, cards.Count);
        LevelUpCardConfig randomElement = cards.ElementAt(index).Value;
        return randomElement;
    }

    public ItemConfig GetRandomItem()
    {
        int index = Random.Range(0, items.Count);
        ItemConfig randomElement = items.ElementAt(index).Value;
        return randomElement;
    }

    public InventoryConfig GetInventoryConfig() => inventoryConfig;
}
