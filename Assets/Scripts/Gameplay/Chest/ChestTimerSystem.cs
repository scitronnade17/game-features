using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ChestTimerSystem : ITickable
{
    private readonly IChestService chestService;
    private readonly ILocalTimeService time;
    private List<string> completeChestTimerBuffer = new(8);
    private float checkCooldown = 1f;

    public ChestTimerSystem(IChestService _chestService,
       ILocalTimeService _time)
    {
        chestService = _chestService;
        time = _time;
    }

    public void Tick()
    {
        checkCooldown -= Time.deltaTime;

        if (checkCooldown > 0f)
            return;

        checkCooldown = 1f;

        var openedChests = chestService.GetOpenedChests();

        if (openedChests.Count == 0)
            return;

        var now = time.LocalTimeNow();
        completeChestTimerBuffer.Clear();

        foreach (KeyValuePair<string, ChestState> keyValuePair in openedChests)
        {
            var id = keyValuePair.Key;
            var chest = keyValuePair.Value;

            if (chest.Timer.IsComplete(now))
                completeChestTimerBuffer.Add(id);
        }

        foreach (var readyId in completeChestTimerBuffer)
        {
            chestService.MarkChestReady(readyId, now);
        }

        completeChestTimerBuffer.Clear();
    }
}