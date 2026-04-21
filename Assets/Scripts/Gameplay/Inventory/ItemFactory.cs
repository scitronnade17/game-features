using UnityEngine;
using Zenject;

public interface IItemFactory
{
    void CreateRandomItem(Vector3 position);
}

public class ItemFactory : IItemFactory
{
    private readonly IConfigDataService configs;
    private readonly IInstantiator instantiator;

    public ItemFactory(IConfigDataService _configs,
       IInstantiator _instantiator)
    {
        configs = _configs;
        instantiator = _instantiator;
    }

    public void CreateRandomItem(Vector3 position)
    {
        ItemConfig config = configs.GetRandomItem();

        ItemWorld item = instantiator.InstantiatePrefabForComponent<ItemWorld>(
            config.Prefab,
            position,
            Quaternion.identity,
            null);

        item.SetKey(config.ItemId);

    }
}