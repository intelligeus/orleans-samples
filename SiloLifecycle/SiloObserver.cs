using Orleans.Runtime;

namespace SiloLifecycle;

public class CallGrainStartupTask : IStartupTask
{
    private readonly IGrainFactory _grainFactory;

    public CallGrainStartupTask(IGrainFactory grainFactory)
    {
        _grainFactory = grainFactory;
    }

    public async Task Execute(CancellationToken cancellationToken)
    {
        Console.WriteLine("YOUR CODE GOES HERE");
        //var grain = grainFactory.GetGrain<MySimpleTestGrain>(2);
        //do something with the grain
    }
    
}

class StartupTask : ILifecycleParticipant<ISiloLifecycle>
{
    
    public void Participate(ISiloLifecycle lifecycle)
    {
        // Couple of helpers so we can see when the stage starts and stops
        Task OnStart(CancellationToken ct)
        {
            Console.WriteLine("Runtime Grain Services Starting");
            return Task.CompletedTask;
        }
        
        Task OnStop(CancellationToken ct)
        {
            Console.WriteLine("Runtime Grain Services Stopping");
            return Task.CompletedTask;
        }
        
        // We are going to hook the ServiceLifecycleStage.RuntimeGrainServices stage
        lifecycle.Subscribe(
            nameof(StartupTask),
            ServiceLifecycleStage.RuntimeGrainServices,
            ct => OnStart(ct),
            ct => OnStop(ct));
    }
}