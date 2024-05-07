using HelloComputer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

// Configure the host using localhost clustering. This is where our grain will execute 
using var host = new HostBuilder()
    .UseOrleans(builder => builder.UseLocalhostClustering())
    .Build();

// Start the host
await host.StartAsync();

// Orleans handles the lifecycle of our grain so we need a service to access it
var grainFactory = host.Services.GetRequiredService<IGrainFactory>();

// Get a reference to the HelloComputer grain with the key "HAL". This grain does not exist
// so Orleans creates an instance
// for us. 
var friend = grainFactory.GetGrain<IHelloComputer>("HAL");

// Call the grain and print the result to the console
var result = await friend.HelloComputer("Dave");
Console.WriteLine($"{result}");

// Shut down the host
Console.WriteLine("Orleans is running.\nPress Enter to terminate...");
Console.ReadLine();
Console.WriteLine("Orleans is stopping...");

await host.StopAsync();