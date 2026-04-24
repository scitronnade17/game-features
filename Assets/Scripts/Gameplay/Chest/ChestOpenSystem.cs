using System;
using Zenject;

public class ChestOpenSystem : IInitializable, IDisposable
{
    private readonly IChestBus bus;
    private readonly IEventBus eventBus;
    private readonly IChestService chestService;
    private readonly ILocalTimeService time;

    public ChestOpenSystem(IChestBus _bus,
       IEventBus _eventBus,
       IChestService _chestService,
       ILocalTimeService _time)
    {
        bus = _bus;
        eventBus = _eventBus;
        chestService = _chestService;
        time = _time;
    }

    public void Initialize()
    {
        chestService.CreateChests();

        bus.OnChestOpenClick += TryOpenChest;
    }

    private void TryOpenChest(string id)
    {
        var nowTime = time.LocalTimeNow();

        if (chestService.TryOpenChest(id, nowTime, out var chest).IsSuccess)
        {
            eventBus.RaiseEvent(new ChestOpenRewardSignal());
        }
    }

    public void Dispose()
    {
        bus.OnChestOpenClick -= TryOpenChest;
    }
}