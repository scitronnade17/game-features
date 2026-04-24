using Zenject;

public class CraftSystemInstaller : Installer<CraftSystemInstaller>
{
    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<CraftEndSystem>().AsSingle();
        Container.BindInterfacesAndSelfTo<TryPutItemToCraftSlotSystem>().AsSingle();
        Container.BindInterfacesAndSelfTo<CraftReturnItemSystem>().AsSingle();
        Container.BindInterfacesAndSelfTo<ReceiptClickSystem>().AsSingle();
        Container.BindInterfacesAndSelfTo<CraftPanelUISystem>().AsSingle();
    }
}