using System.ComponentModel;
using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    [SerializeField] private PlayerFacade playerFacade;
    public override void InstallBindings()
    {
        Container.Bind<PlayerFacade>().FromInstance(playerFacade).AsSingle();
        Container.BindInterfacesAndSelfTo<SaveLoadSystem>().AsSingle();

        BindInventorySystem();
    }

    private void BindInventorySystem()
    {
        Container.Bind<IInventoryGridService>().To<InventoryGridService>().AsSingle();
        Container.Bind<IInventoryStackService>().To<InventoryStackService>().AsSingle();
        Container.Bind<IInventoryFactory>().To<InventoryFactory>().AsSingle();
        Container.Bind<IItemFactory>().To<ItemFactory>().AsSingle();
        Container.BindInterfacesAndSelfTo<ItemCreator>().AsSingle();
        Container.BindInterfacesAndSelfTo<InventoryService>().AsSingle();
        Container.BindInterfacesAndSelfTo<InventoryPickupSystem>().AsSingle();
        Container.BindInterfacesAndSelfTo<InventoryWindowSystem>().AsSingle();
        Container.BindInterfacesAndSelfTo<InventoryItemViewSystem>().AsSingle();
        Container.BindInterfacesAndSelfTo<InventoryPanelService>().AsSingle();
        Container.BindInterfacesAndSelfTo<InventoryPanelPresenter>().AsSingle();


        Container.BindInterfacesAndSelfTo<InventoryStartSystem>().AsSingle();

    }
}