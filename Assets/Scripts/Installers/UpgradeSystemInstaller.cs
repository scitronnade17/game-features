using Zenject;

public class UpgradeSystemInstaller : Installer<UpgradeSystemInstaller>
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<UpgradeSystem>().AsSingle();
        Container.Bind<IUpgradeFactory>().To<UpgradeFactory>().AsSingle();
        Container.BindInterfacesAndSelfTo<LevelUpWindowPresenter>().AsSingle();
    }
}