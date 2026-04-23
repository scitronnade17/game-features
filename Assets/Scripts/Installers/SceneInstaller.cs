using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    [SerializeField] private PlayerFacade playerFacade;
    public override void InstallBindings()
    {
        Container.Bind<PlayerFacade>().FromInstance(playerFacade).AsSingle();
        Container.BindInterfacesAndSelfTo<SaveLoadSystem>().AsSingle();

        InventorySystemInstaller.Install(Container);
        CraftSystemInstaller.Install(Container);
        UpgradeSystemInstaller.Install(Container);
    }
}