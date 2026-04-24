using UnityEngine;
using Zenject;

public class ProjectInstaller : MonoInstaller, ICoroutineRunner
{
    [SerializeField] private LoadingCurtain loadingCurtainPrefab;
    public override void InstallBindings()
    {
        Container.Bind<IConfigDataService>().To<ConfigDataService>().AsSingle().NonLazy();
        Container.Bind<ILocalTimeService>().To<LocalTimeService>().AsSingle();
        Container.Bind<ICoroutineRunner>().FromInstance(this).AsSingle();
        Container.Bind<IDIService>().To<DIService>().AsSingle();
        Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();

        Container.Bind<ILoadingCurtain>()
            .FromComponentInNewPrefab(loadingCurtainPrefab)
            .AsSingle()
            .NonLazy();

        BindSignalBus();
        BindGameStateMachine();
        BindSaveLoadService();
    }

    private void BindSaveLoadService()
    {
        Container.Bind<IProgressService>().To<ProgressService>().AsSingle();
        Container.Bind<ISaveLoadRegistry>().To<SaveLoadRegistry>().AsSingle();
        Container.Bind<ISaveLoadService>().To<SaveLoadService>().AsSingle();
    }

    private void BindGameStateMachine()
    {
        Container.Bind<IStateFactory>().To<StateFactory>().AsSingle();
        Container.BindInterfacesAndSelfTo<GameStateMachine>().AsSingle();

        Container.BindInterfacesAndSelfTo<BootstrapState>().AsSingle();
        Container.BindInterfacesAndSelfTo<LoadLevelState>().AsSingle();
        Container.BindInterfacesAndSelfTo<LevelLoopState>().AsSingle();
    }

    private void BindSignalBus()
    {
        SignalBusInstaller.Install(Container);
        Container.Bind<IEventBus>().To<ZenjectEventBus>().AsSingle().NonLazy();

        Container.DeclareSignal<UpgradeSignal>();
        Container.DeclareSignal<CreateItemSignal>();
        Container.DeclareSignal<ChestOpenRewardSignal>();
        //PauseSignal
    }
}