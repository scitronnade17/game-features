using System;
using System.Collections.Generic;
using UnityEngine;

public interface IChestService
{
    ChestState GetChest(string id);
    bool CanOpenChest(string id);
    ChestResult TryOpenChest(string id, DateTime now, out ChestState state);
    void MarkChestReady(string id, DateTime now);
    void CreateChests();
    IReadOnlyDictionary<string, ChestState> GetOpenedChests();
    IReadOnlyDictionary<string, ChestState> GetAllChests();
    event Action<string, double> OnChestOpen;
    event Action<string, ChestState> OnChestReady;
}

public class ChestService : IChestService, ISaveLoad
{
    public event Action<string, double> OnChestOpen;
    public event Action<string, ChestState> OnChestReady;

    private Dictionary<string, ChestState> chests = new();
    private Dictionary<string, ChestState> openedChests = new();

    private readonly ILocalTimeService time;
    private readonly IChestFactory chestFactory;
    private readonly IConfigDataService config;

    public ChestService(ILocalTimeService _time,
       IChestFactory _chestFactory,
       IConfigDataService _config)
    {
        time = _time;
        chestFactory = _chestFactory;
        config = _config;
    }

    public ChestState GetChest(string id) =>
       chests[id];

    public bool CanOpenChest(string id)
    {
        if (!chests.TryGetValue(id, out var state))
            return false;

        var now = time.LocalTimeNow();
        return state.CanOpen(now);
    }

    public ChestResult TryOpenChest(string id, DateTime now, out ChestState state)
    {
        state = null;

        if (!chests.TryGetValue(id, out var chestState))
            return ChestResult.Fail(ChestFailReason.NotFound);

        if (!chestState.TryOpen(now))
            return ChestResult.Fail(ChestFailReason.NotReady);

        state = chestState;
        openedChests[id] = state;
        OnChestOpen?.Invoke(id, state.Timer.GetRemainTimeInSeconds(now));

        return ChestResult.Success();
    }

    public void MarkChestReady(string uniqueId, DateTime now)
    {
        if (!openedChests.TryGetValue(uniqueId, out var chest))
            return;

        chest.UpdateReadyState(now);

        if (chest.ReadyToOpen)
            openedChests.Remove(uniqueId);

        OnChestReady?.Invoke(uniqueId, chest);
    }

    public void CreateChests()
    {
        foreach (var kv in config.GetAllChests())
        {
            if (!chests.ContainsKey(kv.Key))
            {
                var chestState = chestFactory.CreateChest(kv.Key);
                chests.Add(kv.Key, chestState);
            }
        }
    }

    public IReadOnlyDictionary<string, ChestState> GetOpenedChests() =>
      openedChests;

    public IReadOnlyDictionary<string, ChestState> GetAllChests() =>
       chests;

    public void Save(PlayerProgress progress)
    {
        if (progress == null)
            return;

        progress.ChestData = new ChestData(chests.Values);
    }

    public void Load(PlayerProgress progress)
    {
        chests.Clear();
        openedChests.Clear();

        foreach (ChestState savedChest in progress.ChestData.Chests)
        {
            if (savedChest == null || string.IsNullOrEmpty(savedChest.Id))
                continue;

            chests[savedChest.Id] = savedChest;

            if (!savedChest.ReadyToOpen)
                openedChests[savedChest.Id] = savedChest;
        }
    }
}