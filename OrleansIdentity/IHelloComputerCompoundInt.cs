namespace OrleansIdentity;

public interface IHelloComputerCompoundInt : IGrainWithIntegerCompoundKey
{
    Task<string> HelloComputer(string name);
}