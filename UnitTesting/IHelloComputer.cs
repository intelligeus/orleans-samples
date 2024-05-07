namespace UnitTesting;

public interface IHelloComputer : IGrainWithStringKey
{
    Task<string> HelloComputer(string name);
}