namespace OrleansIdentity;

public interface IHelloComputer : IGrainWithStringKey
{
    Task<string> HelloComputer(string name);
}