using Zenject;

public class SaveLoadSystem : IInitializable
{
    private readonly ISaveLoadService saveLoad;

    public SaveLoadSystem(ISaveLoadService _saveLoad)
    {
        saveLoad = _saveLoad;
    }

    public void Initialize()
    {
        saveLoad.Load();
        saveLoad.Save();
    }

}