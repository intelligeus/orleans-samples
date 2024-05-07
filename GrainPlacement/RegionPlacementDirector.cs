using Orleans.Placement;
using Orleans.Runtime;
using Orleans.Runtime.Placement;

namespace GrainPlacement;


[Serializable]
public sealed class RegionPlacementStrategy : PlacementStrategy
{
}

[AttributeUsage(AttributeTargets.Class)]
public sealed class RegionGrainStrategyAttribute : PlacementAttribute
{
    public RegionGrainStrategyAttribute() :
        base(new RegionPlacementStrategy())
    {
    }
}

public class RegionPlacementDirector : IPlacementDirector
{
    public Task<SiloAddress> OnAddActivation(
        PlacementStrategy strategy,
        PlacementTarget target,
        IPlacementContext context)
    {
        Console.WriteLine("RegionPlacementDirector :: OnAddActivation called");
        var silos = context.GetCompatibleSilos(target).OrderBy(s => s).ToArray();
        var silo = GetSiloForRegion(target.GrainIdentity.Key.ToString());
        Console.WriteLine($"Returning address for Silo [{silo}] for grain activation");

        // Return the selected Silo address and let Orleans instantiate the grain 
        return Task.FromResult(silos[silo]);
    }


    private int GetSiloForRegion(string key)
    {
        // We are using a compound (integer + extension) so we check the extension here 
        // to determine the silo we want the grain activated on
        if (key.EndsWith("+East"))
        {
            return 1;
        }

        return 0;
    }

}