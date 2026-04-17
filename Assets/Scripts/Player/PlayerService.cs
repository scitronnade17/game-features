
using UnityEngine;

public interface IPlayerService
{
    PlayerFacade PlayerFacade { get; }
    void Register(PlayerFacade _playerFacade);
}

public class PlayerService: IPlayerService, ISaveLoad
{
    private PlayerFacade playerFacade;
    public PlayerFacade PlayerFacade => playerFacade;

    public void Register(PlayerFacade _playerFacade)
    {
        playerFacade = _playerFacade;
    }

    public void Save(PlayerProgress progress)
    {
        progress.PlayerData.health = playerFacade.PlayerHealth.GetHealth();
    }

    public void Load(PlayerProgress progress)
    {
        playerFacade.PlayerHealth.SetHealth(progress.PlayerData.health);
    }
}