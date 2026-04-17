using UnityEngine;
using Zenject;

public interface IUpgradeFactory
{
    CardView CreateRandomCard(Transform parent);
}

public class UpgradeFactory : IUpgradeFactory
{
    private readonly IConfigDataService configs;
    private readonly IInstantiator instantiator;

    public UpgradeFactory(IConfigDataService _configs,
       IInstantiator _instantiator)
    {
        configs = _configs;
        instantiator = _instantiator;
    }

    public CardView CreateRandomCard(Transform parent)
    {
        GameObject prefab = Resources.Load<GameObject>("Card");
        LevelUpCardConfig config = configs.GetRandomCard();

        CardView card = instantiator.InstantiatePrefabForComponent<CardView>(prefab, parent);
        card.CardUpgadeId = config.CardId;
        card.SetupCard(
           config.Icon,
           config.Label);

        return card;
    }
}