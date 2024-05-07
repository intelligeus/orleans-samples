namespace StatelessWorkerGrains;

public interface IStatelessWorker : IGrainWithStringKey
{
    Task IncrementRequestCount();
}