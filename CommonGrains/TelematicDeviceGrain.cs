using CommonGrains;
using CommonGrains.Interfaces;
using Orleans.Runtime;

namespace GrainState;

public class TelematicDeviceGrain : Grain, ITelematicDeviceGrain
{
    
    private readonly IPersistentState<DeviceCheckIn> _lastCheckIn;
    private readonly IPersistentState<List<DeviceMessage>> _messages;

    
    public TelematicDeviceGrain([PersistentState("Device-Check-In", "cosmosdb")] IPersistentState<DeviceCheckIn> deviceCheckIns,
        [PersistentState("Device-Messages", "cosmosdb")] IPersistentState<List<DeviceMessage>> messages)
    {
        _lastCheckIn = deviceCheckIns;
        _messages = messages;
    }

    public Task ReceiveMessage(string message)
    {
        Console.WriteLine($"Device {this.GetGrainId()} Received Message @ {DateTime.Now:T} Message : {message} Last Checkin {_lastCheckIn.State.LastCheckInTime}");

        _lastCheckIn.State = new DeviceCheckIn() { DeviceId = this.GetGrainId().ToString(), LastCheckInTime = DateTime.Now };

        _lastCheckIn.WriteStateAsync();
        
        return Task.CompletedTask;
    }

    public Task GenerateMessage()
    {
        var message = new DeviceMessage() { DeviceId = this.GetGrainId().ToString(), LastCheckInTime = _lastCheckIn.State.LastCheckInTime, Message = $"This is a message @ {DateTime.UtcNow}", Location = "I am here"};
        
        _messages.State.Add(message);
        _messages.WriteStateAsync();
        return Task.CompletedTask;
    }

    public Task TraceMessages()
    {
        // These messages have been reloaded from the datastore
        Console.WriteLine();
        Console.WriteLine("Messages retrieved from the data store");
        foreach (var msg in _messages.State)
        {
            Console.WriteLine($"{msg.DeviceId}  {msg.Message} {msg.Location}");
        }
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