namespace CommonGrains.Interfaces;

public interface ITelematicDeviceGrain : IGrainWithIntegerKey
{
    Task ReceiveMessage(string message);
    
    // Generate the message to send to the
    Task GenerateMessage();
    
    Task TraceMessages();
}