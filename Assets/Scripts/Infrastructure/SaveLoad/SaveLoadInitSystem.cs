using Zenject;

public class SaveLoadInitSystem : IInitializable
{
    private readonly IProgressService progress;

    public SaveLoadInitSystem(IProgressService _progress)
    {
        progress = _progress;
    }

    public void Initialize()
    {
        progress.LoadProgressOrInitNew();
    }
}