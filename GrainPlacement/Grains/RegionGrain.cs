using Orleans.Placement;

namespace GrainPlacement.Grains;

[RegionGrainStrategy]
public class RegionGrain : Grain, IRegionGrain
{
    public Task RegionGrainTask()
    {
       Console.WriteLine("I am a Region Grain");
       
       return Task.CompletedTask;
    }
}