using Zenject;

public class InventorySystemInstaller : Installer<InventorySystemInstaller>
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<ItemCreator>().AsSingle();

        Container.BindInterfacesAndSelfTo<InventoryPanelService>().AsSingle();
        Container.BindInterfacesAndSelfTo<InventoryPanelPresenter>().AsSingle();

        Container.BindInterfacesAndSelfTo<InventoryStartSystem>().AsSingle();
        Container.BindInterfacesAndSelfTo<InventoryPickupSystem>().AsSingle();
        Container.BindInterfacesAndSelfTo<InventoryWindowSystem>().AsSingle();
        Container.BindInterfacesAndSelfTo<InventoryItemViewSystem>().AsSingle();
    }
}