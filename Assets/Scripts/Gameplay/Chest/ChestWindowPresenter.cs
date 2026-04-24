using System;
using System.Collections.Generic;
using UnityEngine;

public interface IChestWindowPresenter
{
    void OpenWindow();
    void UpdateTimer(string id, double currentTime);
    void Register(ChestWindow chestWindow);
}

public class ChestWindowPresenter : IChestWindowPresenter, IDisposable
{
    private Dictionary<string, ChestView> chestViews = new();
    private readonly IChestService chestService;
    private IChestFactory chestFactory;
    private ChestWindow chestWindow;

    public ChestWindowPresenter(IChestService _chestService,
       IChestFactory _chestFactory)
    {
        chestService = _chestService;
        chestFactory = _chestFactory;

        chestService.OnChestOpen += UpdateTimer;
        chestService.OnChestReady += ChestReady;
    }

    public void Register(ChestWindow _chestWindow)
    {
        chestWindow = _chestWindow;
    }

    public void OpenWindow()
    {
        foreach (var kv in chestViews)
            GameObject.Destroy(kv.Value.gameObject);

        chestViews.Clear();

        foreach (var kvp in chestService.GetAllChests())
        {
            ChestView chestView = chestFactory.CreateChestView(kvp.Key, chestWindow.ChestParent);

            chestView.UpdateState(kvp.Value);

            chestViews.Add(kvp.Key, chestView);
        }

        chestWindow.UpdateWindow();
    }

    public void UpdateTimer(string id, double currentTime)
    {
        if (chestViews.TryGetValue(id, out var chestView))
        {
            chestView.UpdateTimer(currentTime);
        }
    }

    private void ChestReady(string id, ChestState chest)
    {
        if (chestViews.TryGetValue(id, out var chestView))
            chestView.UpdateState(chest);
    }

    public void Dispose()
    {
        chestService.OnChestOpen -= UpdateTimer;
        chestService.OnChestReady -= ChestReady;
    }
}