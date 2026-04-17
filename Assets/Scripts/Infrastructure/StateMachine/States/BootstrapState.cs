using UnityEngine;

public class BootstrapState : IState
{
    private readonly IGameStateMachine stateMachine;
    private readonly ILoadingCurtain curtain;
    private readonly IConfigDataService configDataService;

    public BootstrapState(
        IGameStateMachine _stateMachine,
        ILoadingCurtain _curtain,
        IConfigDataService _configDataService)
    {
        stateMachine = _stateMachine;
        curtain = _curtain;
        configDataService = _configDataService;
    }

    public void Enter()
    {
        curtain.Show();
        configDataService.Load();

        Debug.Log("Game Warmup Complete");

        stateMachine.Enter<LoadLevelState, int>(1);
    }

    public void Exit() { }
}