using Zenject;

public class CraftInstaller : Installer<CraftInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<ICraftBus>().To<CraftBus>().AsSingle();
        Container.Bind<ICraftFactory>().To<CraftFactory>().AsSingle();
        Container.Bind<ICraftService>().To<CraftService>().AsSingle();
        Container.Bind<ICraftSlotService>().To<CraftSlotService>().AsSingle();

        Container.Bind<ICraftUIService>().To<CraftUIService>().AsSingle();
        Container.Bind<ICraftPanelPresenter>().To<CraftPanelPresenter>().AsSingle();
    }
}