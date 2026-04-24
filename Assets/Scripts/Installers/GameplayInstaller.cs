using Zenject;

public class GameplayInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<PlayerService>().AsSingle();

        InventoryInstaller.Install(Container);
        ChestInstaller.Install(Container);
        CraftInstaller.Install(Container);
    }

}