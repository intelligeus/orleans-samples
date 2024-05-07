namespace GrainState;

public interface ITelematicDeviceGrain : IGrainWithIntegerKey
{
    Task ReceiveMessage(string message);
}