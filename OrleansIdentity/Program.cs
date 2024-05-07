﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OrleansIdentity;

// Configure the host using localhost clustering. This is where our grain will execute 
using var host = new HostBuilder()
    .UseOrleans(builder => builder.UseLocalhostClustering())
    .Build();

// Start the host
await host.StartAsync();

// Orleans handles the lifecycle of our grain so we need a service to access it
var grainFactory = host.Services.GetRequiredService<IGrainFactory>();

// Get a reference to the HelloComputer grain with the key HAL. This grain does not exist
// so Orleans creates an instance
// for us. 
var friend = grainFactory.GetGrain<IHelloComputer>("HAL");

// Call the grain and print the result to the console
var result = await friend.HelloComputer("Dave");
Console.WriteLine($"{result}");


// Get a reference to the HelloComputer grain with the key 1. This grain does not exist
// so Orleans creates an instance
// for us. 
var friendInt = grainFactory.GetGrain<IHelloComputerInt>(1);

// Call the grain and print the result to the console
result = await friendInt.HelloComputer("Dave");
Console.WriteLine($"{result}");


// Get a reference to the HelloComputer grain with the key 1 and extension 'MyStringId'. This grain does not exist
// so Orleans creates an instance
// for us. 
var friendCompound = grainFactory.GetGrain<IHelloComputerCompoundInt>(1, "MyStringId");

// Call the grain and print the result to the console
result = await friendCompound.HelloComputer("Dave");
Console.WriteLine($"{result}");


// Shut down the host
Console.WriteLine("Orleans is running.\nPress Enter to terminate...");
Console.ReadLine();
Console.WriteLine("Orleans is stopping...");

await host.StopAsync();