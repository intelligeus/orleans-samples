using Orleans.Concurrency;

namespace GrainPlacement.Grains;

[StatelessWorker]
public class StatelessWorkerGrain : Grain, IStatelessWorkerGrain
{
    public Task StatelessWorkerGrainTask()
    {
       Console.WriteLine("I am a grain which will prefer local placement if possible");
       
       return Task.CompletedTask;
    }
}