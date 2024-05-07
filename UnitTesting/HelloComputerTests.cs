using Orleans.TestingHost;
using Xunit;

namespace UnitTesting;

[Collection(ClusterCollection.Name)]
public class HelloComputerTests
{

    private readonly TestCluster _cluster;

    public HelloComputerTests(ClusterFixture fixture)
    {
        _cluster = fixture.Cluster;
    }
    
    
    /**
     * This was the original test written to demonstrate the TestCluster
     * The class is now annotated with a fixture which spins up the TestCluster for us
     * so the cluster instantiation here is redundant and _cluster is available
     */
    [Fact]
    public async Task ShouldSayHelloComputer()
    {
        var builder = new TestClusterBuilder();
        var cluster = builder.Build();
        await cluster.DeployAsync();

        var friend = cluster.GrainFactory.GetGrain<IHelloComputer>("HAL");

        var response = await friend.HelloComputer("Dave");
        
        await cluster.StopAllSilosAsync();
        
        Assert.Equal("Affirmative Dave, I read you.", response);

    }
    
    [Fact]
    public async Task ShouldSayHelloComputerUsingFixture()
    {
        
        var friend = _cluster.GrainFactory.GetGrain<IHelloComputer>("HAL");

        var response = await friend.HelloComputer("Dave");
        
        Assert.Equal("Affirmative Dave, I read you.", response);

    }

}