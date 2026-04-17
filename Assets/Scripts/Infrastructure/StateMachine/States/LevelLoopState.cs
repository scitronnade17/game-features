public class LevelLoopState : IState, IUpdatable
{
    private readonly ILoadingCurtain curtain;
    private readonly ILevelUpWindowPresenter levelUpWindowPresenter;

    public LevelLoopState(ILoadingCurtain _curtain, ILevelUpWindowPresenter _levelUpWindowPresenter)
    {
        curtain = _curtain;
        levelUpWindowPresenter = _levelUpWindowPresenter;
    }

    public void Enter()
    {
        curtain.Hide();
        levelUpWindowPresenter.Show();

    }

    public void Tick()
    {
        //Debug.Log("LevelLoop state");
    }

    public void Exit()
    {
    }
}