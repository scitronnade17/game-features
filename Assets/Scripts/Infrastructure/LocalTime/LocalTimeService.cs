using System;

public interface ILocalTimeService
{
    DateTime LocalTimeNow();
    DateTime LocalTimePlusOffset(float seconds);
}

public class LocalTimeService : ILocalTimeService
{
    public DateTime LocalTimeNow()
    {
        return DateTime.Now;
    }

    public DateTime LocalTimePlusOffset(float seconds) =>
       LocalTimeNow() + TimeSpan.FromSeconds(seconds);
}