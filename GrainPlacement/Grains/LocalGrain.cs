using Orleans.Placement;

namespace GrainPlacement.Grains;

[PreferLocalPlacement]
public class LocalGrain : Grain, ILocalGrain
{
    public Task LocalGrainTask()
    {
       Console.WriteLine("I am a grain which will prefer local placement if possible");
       
       return Task.CompletedTask;
    }
}