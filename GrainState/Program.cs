using GrainState;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Orleans.Configuration;


using var host = new HostBuilder()
    .UseOrleans(builder =>
    {
        builder.UseLocalhostClustering();

        builder.AddMemoryGrainStorageAsDefault();
        
        // Force some values for cleanup in the silo so our grain is deactivated
        builder.Configure<GrainCollectionOptions>(options =>
        {
            options.CollectionQuantum = TimeSpan.FromSeconds(5); 
            options.CollectionAge = TimeSpan.FromSeconds(6); // This value must be greater than CollectionQuantum
        }); 
    })
    .Build();

// Start the host
await host.StartAsync();

// Orleans handles the lifecycle of our grain so we need a service to access it
var grainFactory = host.Services.GetRequiredService<IGrainFactory>();

// Get a reference to the grain
var telematicGrain = grainFactory.GetGrain<ITelematicDeviceGrain>(4269867);

await telematicGrain.ReceiveMessage("Telematic device message");

Thread.Sleep(1000);

await telematicGrain.ReceiveMessage("Telematic device message");

Thread.Sleep(1000);

await telematicGrain.ReceiveMessage("Telematic device message");

Thread.Sleep(20000);

await telematicGrain.ReceiveMessage("Telematic device message after deactivation");

Console.WriteLine("Orleans is running.\nPress Enter to terminate...");
Console.ReadLine();

Console.WriteLine("Orleans is stopping...");

await host.StopAsync();