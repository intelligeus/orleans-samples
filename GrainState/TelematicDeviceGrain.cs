using Orleans.Runtime;

namespace GrainState;

public class TelematicDeviceGrain : Grain, ITelematicDeviceGrain
{
    
    private readonly IPersistentState<DeviceCheckIn> _lastCheckIn;

    
    public TelematicDeviceGrain([PersistentState("Device Check In", "")] IPersistentState<DeviceCheckIn> deviceCheckIns)
    {
        _lastCheckIn = deviceCheckIns;
    }

    public Task ReceiveMessage(string message)
    {
        Console.WriteLine($"Device {this.GetGrainId()} Received Message @ {DateTime.Now:T} Message : {message} Last Checkin {_lastCheckIn.State.LastCheckInTime}");

        _lastCheckIn.State = new DeviceCheckIn() { DeviceId = this.GetGrainId().ToString(), LastCheckInTime = DateTime.Now };

        _lastCheckIn.WriteStateAsync();
        
        return Task.CompletedTask;
    }
    
    
    // Override so we can see activations in the trace
    public override Task OnActivateAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Telematic grain is being activated");
        return base.OnActivateAsync(cancellationToken);
    }

    // override so we can de activations in the trace
    public override Task OnDeactivateAsync(DeactivationReason reason, CancellationToken token)
    {
        Console.WriteLine("Telematic Grain is being deactivated");

        return base.OnDeactivateAsync(reason, token);

    }
}