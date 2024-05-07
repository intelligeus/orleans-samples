using System.Net;
using GrainPlacement;
using GrainPlacement.Grains;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Orleans.Runtime;
using Orleans.Runtime.Placement;


int siloPort = 111111;
int gatewayPort = 30000;

try
{
    siloPort = int.Parse(args[0]);
    gatewayPort = int.Parse(args[1]);
}
catch (Exception)
{
    siloPort = 11111;
    gatewayPort = 30000;
}

Console.WriteLine($"Starting Silo on {siloPort} with Gateway Port {gatewayPort}");


using var host = new HostBuilder()
    .UseOrleans(builder =>
    {
        builder.UseLocalhostClustering( siloPort:siloPort, gatewayPort:gatewayPort, 
                primarySiloEndpoint: new IPEndPoint(IPAddress.Loopback, 11111));
        
        // Swap out the default placement strategy (Random) for ActivationCountBasedPlacement
        builder.ConfigureServices(services =>
        {
            services.AddSingletonNamedService<PlacementStrategy, RegionPlacementStrategy>(
                nameof(RegionPlacementStrategy));
            services.AddSingletonKeyedService<
                Type, IPlacementDirector, RegionPlacementDirector>(
                typeof(RegionPlacementStrategy));
        });
        
        builder.AddMemoryGrainStorageAsDefault();

    })
    .Build();

await host.StartAsync();

// var grainFactory = host.Services.GetRequiredService<IGrainFactory>();
//
// // Get a reference to the grain
// var regionGrain = grainFactory.GetGrain<IRegionGrain>(32432432, "East");
//
// await regionGrain.RegionGrainTask();

Console.WriteLine("Orleans is running.\nPress Enter to terminate...");
Console.ReadLine();

Console.WriteLine("Orleans is stopping...");

await host.StopAsync();


