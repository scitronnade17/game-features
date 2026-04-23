using System;

public class PlayerProgress
{
    public PlayerData PlayerData;

    public PlayerProgress()
    {
        PlayerData = new PlayerData();
    }
}

[Serializable]
public class PlayerData
{
    public int health;
}