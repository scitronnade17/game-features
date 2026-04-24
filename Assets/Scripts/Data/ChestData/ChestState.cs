using Newtonsoft.Json;
using System;

[Serializable]
public class ChestState
{
    public string Id { get; private set; }
    public string Name;
    public float DelayTimeAfterOpen;
    public TimerData Timer;
    public bool ReadyToOpen;

    [JsonConstructor]
    public ChestState(
       string id,
       string name,
       float delayTimeAfterOpen,
       TimerData timer,
       bool readyToOpen)
    {
        Id = id;
        Name = name;
        DelayTimeAfterOpen = delayTimeAfterOpen;
        Timer = timer ?? new TimerData();
        ReadyToOpen = readyToOpen;
    }

    public ChestState(
       string id,
       string name,
       float delayTimeAfterOpen)
    {
        Id = id;
        Name = name;
        DelayTimeAfterOpen = delayTimeAfterOpen;
        Timer = new TimerData();
        ReadyToOpen = true;
    }

    public bool CanOpen(DateTime now) =>
       ReadyToOpen && Timer.IsComplete(now);

    public bool TryOpen(DateTime now)
    {
        if (!CanOpen(now))
            return false;

        Open(now);
        return true;
    }

    private void Open(DateTime now)
    {
        ReadyToOpen = false;
        Timer.SetEndTime(now.AddSeconds(DelayTimeAfterOpen));
    }

    public void MakeReady() =>
       ReadyToOpen = true;

    public void ResetTimer() =>
       Timer.Reset();

    public void UpdateReadyState(DateTime now)
    {
        if (!ReadyToOpen && Timer.IsComplete(now))
            ReadyToOpen = true;
    }
}