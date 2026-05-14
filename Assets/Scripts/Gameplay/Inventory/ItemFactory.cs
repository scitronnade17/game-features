using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public interface IItemFactory
{
    Task CreateRandomItem(Vector3 position);
}

public class ItemFactory : IItemFactory
{
    private readonly IConfigDataService configs;
    private readonly IInstantiator instantiator;
    private readonly IAssetProvider assetProvider;

    public ItemFactory(IConfigDataService _configs,
       IInstantiator _instantiator,
       IAssetProvider _assetProvider)
    {
        configs = _configs;
        instantiator = _instantiator;
        assetProvider = _assetProvider;
    }

    public async Task CreateRandomItem(Vector3 position)
    {
        ItemConfig config = configs.GetRandomItem();

        GameObject prefab = await assetProvider.Load<GameObject>(config.Prefab);

        ItemWorld item = instantiator.InstantiatePrefabForComponent<ItemWorld>(
            prefab,
            position,
            Quaternion.identity,
            null);

        item.SetKey(config.ItemId);

    }
}