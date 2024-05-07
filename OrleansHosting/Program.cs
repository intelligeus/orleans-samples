// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OrleansHosting;

// Configure the host using localhost clustering. This is where our grain will execute 
using var host = new HostBuilder()
    .UseOrleans(builder => builder.UseLocalhostClustering())
    .Build();

// Start the host
await host.StartAsync();

// Orleans handles the lifecycle of our grain so we need a service to access it
var grainFactory = host.Services.GetRequiredService<IGrainFactory>();

// Get a reference to the Periodic grain. This grain does not exist
// so Orleans creates an instance for us. We pass in "1" as the primary key.
var periodic = grainFactory.GetGrain<IPeriodic>("1");

// // Call the grain to activate it and trigger the timer instantiation
await periodic.ActivateMe();
Console.WriteLine("Orleans is running and thead will sleep");

Thread.Sleep(10000);

Console.WriteLine("Orleans is stopping...");
await host.StopAsync();
