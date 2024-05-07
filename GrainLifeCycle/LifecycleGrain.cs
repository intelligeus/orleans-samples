using Orleans.Runtime;

namespace GrainLifeCycle;

public class LastUserState 
{
    public string LastName { get; set; }

}


public class HelloComputerObserver : ILifecycleParticipant<IGrainLifecycle>
{
    // Create an instance of our component and hook up our methods
    public static HelloComputerObserver Create(IGrainContext context)
    {
        Console.WriteLine("Create called on HelloComputerObserver");
        var component = new HelloComputerObserver();
        component.Participate(context.ObservableLifecycle);
        return component;
    }
    
    private Task OnFirstState(CancellationToken token)
    {
        Console.WriteLine("First state called on grain");
        return Task.CompletedTask;
    }
    
    private Task OnSetupState(CancellationToken token)
    {
        Console.WriteLine("Setup state called on grain");
        return Task.CompletedTask;
    }

    private Task Activate(CancellationToken token)
    {
        Console.WriteLine("Activate called on grain");
        return Task.CompletedTask;
    }
    
    // Specify the lifecycle stages we wish to capture 
    public void Participate(IGrainLifecycle observer)
    {
        Console.WriteLine("Participate called on HelloComputerObserver");
        observer.Subscribe<HelloComputerObserver>(
            GrainLifecycleStage.First,
            OnFirstState);
        
        observer.Subscribe<HelloComputerObserver>(
            GrainLifecycleStage.SetupState,
            OnSetupState);
        
        observer.Subscribe<HelloComputerObserver>(
            GrainLifecycleStage.Activate,
            Activate);
    }
    
}


public class HelloComputerGrain : Grain<LastUserState>, IHelloComputerGrain
{
    private readonly HelloComputerObserver _observer;

    public HelloComputerGrain(HelloComputerObserver observer)
    {
        _observer = observer;
    }

    public Task HelloComputer()
    {
        Console.WriteLine("Hello Computer method called");
        
        return Task.CompletedTask;
    }
    
}