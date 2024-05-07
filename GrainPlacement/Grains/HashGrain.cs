using Orleans.Placement;

namespace GrainPlacement.Grains;

[HashBasedPlacement]
public class HashGrain : Grain, IHashGrain
{
    public Task HashGrainTask()
    {
       Console.WriteLine("I am a grain placed with the hashing algorithm");
       
       return Task.CompletedTask;
    }
}