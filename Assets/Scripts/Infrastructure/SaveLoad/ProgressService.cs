using Newtonsoft.Json;
using UnityEngine;

public interface IProgressService
{
    PlayerProgress Progress { get; }
    bool HasLoadProgress { get; }

    PlayerProgress CreateNewProgress();
    PlayerProgress LoadProgressOrInitNew();
    void SaveProgress();
}

public class ProgressService: IProgressService
{
    public const string ProgressKey = "Progress";
    public PlayerProgress Progress { get; private set; }
    public bool HasLoadProgress { get; private set; }

    public PlayerProgress CreateNewProgress()
    {
        HasLoadProgress = false;
        return Progress = new PlayerProgress();
    }

    public void SaveProgress()
    {
        var json = JsonConvert.SerializeObject(Progress);
        PlayerPrefs.SetString(ProgressKey, json);
        PlayerPrefs.Save();
    }

    public PlayerProgress LoadProgressOrInitNew()
    {
        var json = PlayerPrefs.GetString(ProgressKey);
        if (string.IsNullOrEmpty(json))
        {
            Debug.Log("No data found");
            return CreateNewProgress();
        }
        else
        {
            Debug.Log("Data found!");
            HasLoadProgress = true;
            Progress = JsonConvert.DeserializeObject<PlayerProgress>(json);
            return Progress;
        }
    }
}