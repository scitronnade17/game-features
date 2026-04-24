using System;
using Newtonsoft.Json;

[Serializable]
public class TimerData
{
    [JsonProperty]
    public DateTime EndTime { get; private set; }

    public void SetEndTime(DateTime endTime) =>
       EndTime = endTime;

    public bool IsComplete(DateTime now) =>
       now >= EndTime;

    public double GetRemainTimeInSeconds(DateTime now)
    {
        if (IsComplete(now))
            return 0;

        return (EndTime - now).TotalSeconds;
    }

    public double GetRemainTimeInSeconds()
    {
        if (IsComplete(DateTime.Now))
            return 0;

        return (EndTime - DateTime.Now).TotalSeconds;
    }

    public void Reset() =>
       EndTime = default;
}