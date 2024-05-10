// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ReentrantGrains;


// Configure the host using localhost clustering. This is where our grain will execute 
using var host = new HostBuilder()
    .UseOrleans(builder =>
        {
            builder.UseLocalhostClustering()
                .AddMemoryGrainStorageAsDefault();
        }
    ) 
    .Build();

// Start the host
await host.StartAsync();

// Orleans handles the lifecycle of our grain so we need a service to access it
var grainFactory = host.Services.GetRequiredService<IGrainFactory>();

// Get a reference to the grain
var grain = grainFactory.GetGrain<IInterleavedMethodGrain>("1");

Stopwatch stopWatch = new Stopwatch();
stopWatch.Start();
// Call the non interleaved method which will execute sequentially

/*
await Task.WhenAll(grain.DoWork(), grain.DoWork());
var workTime = stopWatch.Elapsed;
Console.WriteLine($"Work seconds elapsed {workTime.TotalMilliseconds/1000} ");
// Now call the important method
stopWatch.Restart();
await Task.WhenAll(grain.DrinkBeer(), grain.DrinkBeer(), grain.DrinkBeer(), grain.DrinkBeer());
var  beerTime = stopWatch.Elapsed;
Console.WriteLine($"Beer seconds elapsed {beerTime.TotalMilliseconds/1000} ");
*/


var mayInterleave = grainFactory.GetGrain<IMayInterleaveGrain>("1");

stopWatch.Start();

//await Task.WhenAll(grain.CallMayInterleave(mayInterleave), grain.CallMayInterleave(mayInterleave), grain.CallMayInterleave(mayInterleave), grain.CallMayInterleave(mayInterleave));
// Calling can interleave
await Task.WhenAll(mayInterleave.DoWork("interleave"), mayInterleave.DoWork("interleave"), mayInterleave.DoWork("interleave"));
var  mayReenter = stopWatch.Elapsed;
Console.WriteLine($"May Interleave seconds elapsed {mayReenter.TotalMilliseconds/1000} ");

stopWatch.Restart();
// Calling may not interleave
await Task.WhenAll(mayInterleave.DoWork("donotinterleave"),mayInterleave.DoWork("donotinterleave"));
var  mayNotReenter = stopWatch.Elapsed;
Console.WriteLine($"Not reentrant seconds elapsed {mayNotReenter.TotalMilliseconds/1000} ");

stopWatch.Stop();


// Shut down the host
Console.WriteLine("Orleans is running.\nPress Enter to terminate...");
Console.ReadLine();
Console.WriteLine("Orleans is stopping...");

await host.StopAsync();