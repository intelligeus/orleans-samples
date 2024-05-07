using Orleans.TestKit;
using Xunit;

namespace UnitTesting;

public class HelloComputerTestkit : TestKitBase
{
    
    [Fact]
    public async Task GrainActivation()
    {
        var grain = await Silo.CreateGrainAsync<HelloTestKitGrain>("HAL");

        Assert.Equal(1, grain.ActivateCount);
    }
    
    [Fact]
    public async Task ShouldSayHelloFromTestKit()
    {
        var grain = await Silo.CreateGrainAsync<HelloTestKitGrain>("HAL");

        var response = grain.HelloComputer("TestKit");
        
        Assert.Equal("Affirmative TestKit, I read you.", response.Result);
    }
    
}