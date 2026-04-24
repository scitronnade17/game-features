using Zenject;

public class ChestInstaller : Installer<ChestInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<IChestBus>().To<ChestBus>().AsSingle();
        Container.Bind<IChestFactory>().To<ChestFactory>().AsSingle();
        Container.BindInterfacesAndSelfTo<ChestService>().AsSingle();
        Container.Bind<IChestWindowPresenter>().To<ChestWindowPresenter>().AsSingle();
    }
}