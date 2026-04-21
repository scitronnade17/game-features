using System;
using UnityEngine;

public interface IItemCreator
{
    void CreateItem(CreateItemSignal signal);
}

public class ItemCreator : IItemCreator, IDisposable
{
    private readonly IItemFactory itemFactory;
    private readonly IEventBus eventBus;

    public ItemCreator(IItemFactory _itemFactory, IEventBus _eventBus)
    {
        itemFactory = _itemFactory;
        eventBus = _eventBus;
        eventBus.Subscribe<CreateItemSignal>(CreateItem);
    }

    public void CreateItem(CreateItemSignal signal)
    {
        Vector3 createPos = new Vector3(0, 5, 0);
        itemFactory.CreateRandomItem(createPos);
    }

    public void Dispose()
    {
        eventBus.Unsubscribe<CreateItemSignal>(CreateItem);
    }

}