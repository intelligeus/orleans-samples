namespace GrainPlacement.Grains;

public interface IStatelessWorkerGrain
{
    Task StatelessWorkerGrainTask();
}