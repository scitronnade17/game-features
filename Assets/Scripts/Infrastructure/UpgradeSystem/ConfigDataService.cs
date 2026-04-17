using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public interface IConfigDataService
{
    void Load();
    LevelUpCardConfig GetLevelUpCardConfig(CardUpgradeId id);
    LevelUpCardConfig GetRandomCard();

}

public class ConfigDataService : IConfigDataService
{
    private Dictionary<CardUpgradeId, LevelUpCardConfig> cards = new();

    public void Load()
    {
        cards = Resources.LoadAll<LevelUpCardConfig>("Configs/Cards")
            .ToDictionary(x => x.CardId, x => x);
    }

    public LevelUpCardConfig GetLevelUpCardConfig(CardUpgradeId id) =>
     cards.TryGetValue(id, out LevelUpCardConfig config)
        ? config
        : null;

    public LevelUpCardConfig GetRandomCard()
    {
        int index = Random.Range(0, cards.Count);
        LevelUpCardConfig randomElement = cards.ElementAt(index).Value;
        return randomElement;
    }
}
