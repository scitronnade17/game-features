using Newtonsoft.Json;
using System;

public class PlayerProgress
{
    public PlayerData PlayerData;
    public ChestData ChestData;
    
    [JsonConstructor]
    public PlayerProgress(
         PlayerData playerData,
         ChestData chestData)
    {
        PlayerData = playerData ?? new PlayerData();
        ChestData = chestData ?? new ChestData();
    }

    public PlayerProgress()
    {
        PlayerData = new PlayerData();
        ChestData = new ChestData();
    }
}

[Serializable]
public class PlayerData
{
    public int health;
}