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
        PlayerPrefs.SetString(ProgressKey, JsonUtility.ToJson(Progress));
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
            Progress = JsonUtility.FromJson<PlayerProgress>(json);
            return Progress;
        }
    }
}