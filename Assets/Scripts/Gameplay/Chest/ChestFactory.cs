using UnityEngine;

public interface IChestFactory
{
    ChestView CreateChestView(string id, Transform parent);
    ChestState CreateChest(string id);
}

public class ChestFactory: IChestFactory
{
    private readonly IConfigDataService config;
    private readonly IDIService di;

    public ChestFactory(IConfigDataService _config, IDIService _di)
    {
        config = _config;
        di = _di;
    }

    public ChestState CreateChest(string id)
    {
        var conf = config.GetChest(id);

        var chestState = new ChestState(
            conf.Id,
            conf.Name,
            conf.DelayTimeAfterOpen);

        return chestState;
    }

    public ChestView CreateChestView(string id, Transform parent)
    {
        var conf = config.GetChest(id);

        GameObject prefab = Resources.Load<GameObject>("Chest");
        var chestObject = Object.Instantiate(prefab, parent);
        var chestView = chestObject.GetComponent<ChestView>();

        chestView.Setup(
            conf.Id,
            conf.Name,
            conf.IconClosed,
            conf.IconOpened);

        di.Container.InjectGameObject(chestObject);

        return chestView;
    }
}