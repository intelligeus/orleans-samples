namespace Timers;

public interface IPeriodic : IGrainWithStringKey
{
    Task TimerCallback(object state);

    Task ActivateMe();

}