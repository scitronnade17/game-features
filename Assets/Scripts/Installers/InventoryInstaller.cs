using Zenject;

public class InventoryInstaller : Installer<InventoryInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();
        Container.Bind<IInventoryDropTargetResolver>().To<InventoryDropTargetResolver>().AsSingle();
        Container.Bind<IInventoryBus>().To<InventoryBus>().AsSingle();
        Container.Bind<IInventoryFactory>().To<InventoryFactory>().AsSingle();
        Container.Bind<IItemFactory>().To<ItemFactory>().AsSingle();
        Container.Bind<IInventoryGridService>().To<InventoryGridService>().AsSingle();
        Container.Bind<IInventoryStackService>().To<InventoryStackService>().AsSingle();
        Container.BindInterfacesAndSelfTo<InventoryService>().AsSingle();
    }
}