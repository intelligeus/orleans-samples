namespace OrleansIdentity;

public interface IHelloComputerInt : IGrainWithIntegerKey
{
    Task<string> HelloComputer(string name);
}