using Orleans.Concurrency;

namespace ReentrantGrains;

public interface IInterleavedMethodGrain : IGrainWithStringKey
{
    Task DoWork();

    [AlwaysInterleave]
    Task DrinkBeer();

    Task CallMayInterleave(IMayInterleaveGrain grain);
}