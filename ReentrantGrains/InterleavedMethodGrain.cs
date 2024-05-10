namespace ReentrantGrains;

[Interleave]
public class InterleavedMethodGrain : Grain, IInterleavedMethodGrain
{
    public async Task DoWork()
    {
        await Task.Delay(TimeSpan.FromSeconds(15));
    }

    public async Task DrinkBeer()
    {
        await Task.Delay(TimeSpan.FromSeconds(10));
    }
    
    [Interleave]
    public async Task CallMayInterleave(IMayInterleaveGrain grain)
    {
        await grain.DoWork("interleave");
    }
}