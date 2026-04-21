using System;
using System.Collections.Generic;

public interface ILevelUpWindowPresenter
{
    void Show();
    void Hide(UpgradeSignal signal);
    void Register(LevelUpWindow levelUpWindow);
}

public class LevelUpWindowPresenter : ILevelUpWindowPresenter, IDisposable
{
    private readonly IUpgradeFactory upgradeFactory;
    private List<CardView> cards = new();
    private LevelUpWindow levelUpWindow;
    private IEventBus eventBus;

    public LevelUpWindowPresenter(IUpgradeFactory _upgradeFactory, IEventBus _eventBus)
    {
        upgradeFactory = _upgradeFactory;
        eventBus = _eventBus;
        eventBus.Subscribe<UpgradeSignal>(Hide);
    }

    public void Register(LevelUpWindow _levelUpWindow)
    {
        levelUpWindow = _levelUpWindow;
    }

    public void Show()
    {
        cards.Add(upgradeFactory.CreateRandomCard(levelUpWindow.transform));
        cards.Add(upgradeFactory.CreateRandomCard(levelUpWindow.transform));
        cards.Add(upgradeFactory.CreateRandomCard(levelUpWindow.transform));
        levelUpWindow.ShowLevelUpWindow();
    }

    public void Hide(UpgradeSignal signal)
    {
        levelUpWindow.CloseLevelUpWindow();
        cards.Clear();
    }

    public void Dispose()
    {
        eventBus.Unsubscribe<UpgradeSignal>(Hide);
    }
}
