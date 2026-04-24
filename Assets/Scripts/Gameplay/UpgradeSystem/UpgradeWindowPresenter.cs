using System;
using System.Collections.Generic;
using Zenject;

public interface IUpgradeWindowPresenter
{
    void Show(ChestOpenRewardSignal signal);
    void Hide(UpgradeSignal signal);
    void Register(UpgradeWindow upgradeWindow);
}

public class UpgradeWindowPresenter : IUpgradeWindowPresenter,  IDisposable
{
    private readonly IUpgradeFactory upgradeFactory;
    private UpgradeWindow upgradeWindow;
    private IEventBus eventBus;

    public UpgradeWindowPresenter(IUpgradeFactory _upgradeFactory, IEventBus _eventBus)
    {
        upgradeFactory = _upgradeFactory;
        eventBus = _eventBus;
        eventBus.Subscribe<UpgradeSignal>(Hide);
        eventBus.Subscribe<ChestOpenRewardSignal>(Show);
    }

    public void Register(UpgradeWindow _upgradeWindow)
    {
        upgradeWindow = _upgradeWindow;
    }

    public void Show(ChestOpenRewardSignal signal)
    {
        upgradeWindow.CreateCard(upgradeFactory.CreateRandomCard(upgradeWindow.transform));
        upgradeWindow.CreateCard(upgradeFactory.CreateRandomCard(upgradeWindow.transform));
        upgradeWindow.CreateCard(upgradeFactory.CreateRandomCard(upgradeWindow.transform));
        upgradeWindow.ShowUpgradeWindow();
    }

    public void Hide(UpgradeSignal signal)
    {
        upgradeWindow.CloseUpgradeWindow();
    }

    public void Dispose()
    {
        eventBus.Unsubscribe<UpgradeSignal>(Hide);
        eventBus.Unsubscribe<ChestOpenRewardSignal>(Show);
    }
}
