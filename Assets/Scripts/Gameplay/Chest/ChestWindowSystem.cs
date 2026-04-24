using System;
using UnityEngine;
using Zenject;

public class ChestWindowSystem : IInitializable, ITickable, IDisposable
{
    private readonly IChestBus chestBus;
    private readonly IChestWindowPresenter chestWindow;

    public ChestWindowSystem(IChestBus _chestBus,
       IChestWindowPresenter _chestWindow)
    {
        chestBus = _chestBus;
        chestWindow = _chestWindow;
    }

    public void Initialize()
    {
        chestBus.OnOpenChestsWindowClick += OpenChestsWindow;
    }

    public void Tick()
    {
        if (Input.GetKeyDown(KeyCode.O))
            chestWindow.OpenWindow();
    }

    private void OpenChestsWindow()
    {
        chestWindow.OpenWindow();
    }

    public void Dispose()
    {
        chestBus.OnOpenChestsWindowClick -= OpenChestsWindow;
    }

}