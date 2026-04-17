using System;
using UnityEngine;

public class UpgradeSystem : IDisposable
{
    private readonly IPlayerService playerService;
    private readonly IConfigDataService config;
    private readonly IEventBus eventBus;
    public UpgradeSystem(IPlayerService _playerService, IConfigDataService _config, IEventBus _eventBus)
    {
        playerService = _playerService;
        config = _config;
        eventBus = _eventBus;

        eventBus.Subscribe<UpgradeSignal>(Upgrade);
    }

    public void Upgrade(UpgradeSignal signal)
    {
        LevelUpCardConfig conf = config.GetLevelUpCardConfig(signal.UpgradeId);
        if (signal.UpgradeId == CardUpgradeId.Health10)
        {
            playerService.PlayerFacade.PlayerHealth.UpgradeHealth(conf.Amount);
            Debug.Log("Player health was upgraded! Check player's inspector");
        }
        if (signal.UpgradeId == CardUpgradeId.Speed10)
            Debug.Log("Player speed was upgraded!");
        if (signal.UpgradeId == CardUpgradeId.Damage10)
            Debug.Log("Player damage was upgraded!");
    }

    public void Dispose()
    {
        eventBus.Unsubscribe<UpgradeSignal>(Upgrade);
    }

}