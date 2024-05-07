using Orleans.Placement;

namespace GrainPlacement.Grains;

[RandomPlacement]
public class RandomGrain : Grain, IRandomGrain
{
    public Task RandomGrainTask()
    {
       Console.WriteLine("I am a randomly placed grain");
       
       return Task.CompletedTask;
    }
}