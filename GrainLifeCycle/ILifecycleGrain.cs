namespace GrainLifeCycle;

public interface IHelloComputerGrain : IGrainWithStringKey
{
    Task HelloComputer();
}