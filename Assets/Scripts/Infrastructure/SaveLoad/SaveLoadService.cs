using UnityEngine;

public interface ISaveLoadService
{
    void Save();
    void Load();
}

public class SaveLoadService : ISaveLoadService
{
    private readonly IProgressService progress;
    private readonly ISaveLoadRegistry registry;

    public SaveLoadService(IProgressService _progress, ISaveLoadRegistry _registry)
    {
        progress = _progress;
        registry = _registry;
    }

    public void Save()
    {
        foreach (ISaveLoad saveLoad in registry.GetSaveLoadSevices())
        {
            saveLoad.Save(progress.Progress);
        }

        progress.SaveProgress();
    }
    public void Load()
    {
        if (progress.HasLoadProgress)
        {
            foreach (ISaveLoad saveLoad in registry.GetSaveLoadSevices())
            {
                saveLoad.Load(progress.Progress);
            }
        }
        else
        {
            Debug.Log("No data");
        }

    }

}