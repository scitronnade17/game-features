using Zenject;

public class ChestSystemInstaller : Installer<ChestSystemInstaller>
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<ChestWindowSystem>().AsSingle();
        Container.BindInterfacesAndSelfTo<ChestOpenSystem>().AsSingle();
        Container.BindInterfacesAndSelfTo<ChestTimerSystem>().AsSingle();
        Container.BindInterfacesAndSelfTo<ChestTimerViewSystem>().AsSingle();
    }
}