namespace Timers;

public class PeriodicGrain : Grain, IPeriodic
{

    private int _invocationCounter = 1;

    public Task TimerCallback(object state)
    {
        Console.WriteLine($"Timer callback invoked @ {DateTime.Now:T} This callback has been invoked {_invocationCounter} times");

        _invocationCounter++;
        return Task.CompletedTask;
    }

    
    // We need to activate the grain to register the timer
    public Task ActivateMe()
    {
        return Task.CompletedTask;
    }

    /*
     * We hook the grain activation call to register out timer. We pass in the following
     *      TimerCallback - The method we want to invoke
     *      Null - we are not interested in state for this example
     *      A 2second timespan for the due time ie start the timer in 2 secs
     *      A 3 second timespan for the period between invocations
     */
    public override Task OnActivateAsync(CancellationToken token)
    {
        Console.WriteLine("Registering callback timer");
        RegisterTimer(TimerCallback, new object(), TimeSpan.FromSeconds(2), TimeSpan.FromSeconds(3));

        return base.OnActivateAsync(token);

    }
}