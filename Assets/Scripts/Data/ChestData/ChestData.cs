using System;
using System.Collections.Generic;
using Newtonsoft.Json;

[Serializable]
public sealed class ChestData
{
    public List<ChestState> Chests { get; private set; }

    [JsonConstructor]
    public ChestData(List<ChestState> chests)
    {
        Chests = chests ?? new List<ChestState>();
    }

    public ChestData(IEnumerable<ChestState> chests)
    {
        Chests = chests != null
           ? new List<ChestState>(chests)
           : new List<ChestState>();
    }

    public ChestData()
    {
        Chests = new List<ChestState>();
    }
}