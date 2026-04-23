using Zenject;

public class CraftSystemInstaller : Installer<CraftSystemInstaller>
{
    public override void InstallBindings()
    {
        Container.Bind<ICraftBus>().To<CraftBus>().AsSingle();
        Container.Bind<IInventoryDropTargetResolver>().To<InventoryDropTargetResolver>().AsSingle();
        Container.Bind<ICraftFactory>().To<CraftFactory>().AsSingle();
        Container.Bind<ICraftService>().To<CraftService>().AsSingle();
        Container.Bind<ICraftSlotService>().To<CraftSlotService>().AsSingle();
        Container.Bind<ICraftUIService>().To<CraftUIService>().AsSingle();
        Container.BindInterfacesAndSelfTo<CraftEndSystem>().AsSingle();
        Container.BindInterfacesAndSelfTo<TryPutItemToCraftSlotSystem>().AsSingle();
        Container.BindInterfacesAndSelfTo<CraftReturnItemSystem>().AsSingle();
        Container.BindInterfacesAndSelfTo<ReceiptClickSystem>().AsSingle();
        Container.BindInterfacesAndSelfTo<CraftPanelPresenter>().AsSingle();
        Container.BindInterfacesAndSelfTo<CraftPanelUISystem>().AsSingle();
    }
}