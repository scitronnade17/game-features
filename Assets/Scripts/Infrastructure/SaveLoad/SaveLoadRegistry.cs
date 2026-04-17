using System.Collections.Generic;

public interface ISaveLoadRegistry
{
    IEnumerable<ISaveLoad> GetSaveLoadSevices();
}


public class SaveLoadRegistry : ISaveLoadRegistry
{
    private readonly IDIService di;
    public SaveLoadRegistry(IDIService _di)
    {
        di = _di;
    }

    public IEnumerable<ISaveLoad> GetSaveLoadSevices() => di.ResolveAll<ISaveLoad>();

}
