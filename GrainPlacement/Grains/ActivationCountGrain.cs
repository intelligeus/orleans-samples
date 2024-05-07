using Orleans.Placement;

namespace GrainPlacement.Grains;

[ActivationCountBasedPlacement]
public class ActivationCountGrain : Grain, IActivationCountGrain
{
    public Task ActivationCountGrainTask()
    {
       Console.WriteLine("I am a grain placed with the Activation Count strategy");
       
       return Task.CompletedTask;
    }
}