using System;

public interface IChestBus
{
    event Action<string> OnChestOpenClick;
    event Action OnOpenChestsWindowClick;
    void ChestOpenClick(string id);
    void ChestCreateClick();
}

public class ChestBus : IChestBus
{
    public event Action<string> OnChestOpenClick;
    public event Action OnOpenChestsWindowClick;
    public void ChestCreateClick() => OnOpenChestsWindowClick?.Invoke();

    public void ChestOpenClick(string id) => OnChestOpenClick?.Invoke(id);

}