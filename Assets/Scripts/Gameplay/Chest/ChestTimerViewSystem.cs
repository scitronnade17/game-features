using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ChestTimerViewSystem : ITickable
{
    private readonly IChestService chestService;
    private readonly ILocalTimeService time;
    private readonly IChestWindowPresenter presenter;

    private float updateCooldown = 1f;

    public ChestTimerViewSystem(IChestService _chestService,
       ILocalTimeService _time,
       IChestWindowPresenter _presenter)
    {
        chestService = _chestService;
        time = _time;
        presenter = _presenter;
    }

    public void Tick()
    {
        updateCooldown -= Time.deltaTime;

        if (updateCooldown > 0f)
            return;

        updateCooldown = 1f;

        var openedChests = chestService.GetOpenedChests();
        if (openedChests.Count == 0)
            return;

        var now = time.LocalTimeNow();

        foreach (KeyValuePair<string, ChestState> keyValuePair in openedChests)
        {
            var id = keyValuePair.Key;
            var chest = keyValuePair.Value;

            var secondLeft = chest.Timer.GetRemainTimeInSeconds(now);
            presenter.UpdateTimer(id, secondLeft);
        }
    }
}