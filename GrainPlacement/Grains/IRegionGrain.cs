namespace GrainPlacement.Grains;

public interface IRegionGrain : IGrainWithIntegerCompoundKey
{
    Task RegionGrainTask();
}