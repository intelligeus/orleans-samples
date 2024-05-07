// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Orleans.Runtime;
using SiloLifecycle;

using var host = new HostBuilder()
    .UseOrleans(builder =>
    {
        builder.UseLocalhostClustering();
        builder.AddStartupTask<CallGrainStartupTask>();
        builder.AddStartupTask(
            async (services, cancellation) =>
            {
                var grainFactory = services.GetRequiredService<IGrainFactory>();
                Console.WriteLine("MORE CODE GOES HERE");
                //var grain = grainFactory.GetGrain<IMySimpleTestGrain>(1);
                //do something with the grain
            });
        
            builder.ConfigureServices(services =>
            {
                services.AddTransient<ILifecycleParticipant<ISiloLifecycle>>(serviceProvider => new StartupTask());
            });
    })
    
    .Build();

// Start the host
await host.StartAsync();

// Shut down the host
Console.WriteLine("Orleans is running.\nPress Enter to terminate...");
Console.ReadLine();
Console.WriteLine("Orleans is stopping...");

await host.StopAsync();