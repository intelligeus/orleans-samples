
using GrainPlacement.Grains;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using IHost host = Host.CreateDefaultBuilder(args)
    .UseOrleansClient((ctx, clientBuilder) => clientBuilder.UseLocalhostClustering(30000,"dev", "dev"))
    .UseConsoleLifetime()
    .Build();

await host.StartAsync();

IHostApplicationLifetime lifetime = host.Services.GetRequiredService<IHostApplicationLifetime>();
IClusterClient client = host.Services.GetRequiredService<IClusterClient>();

var grain = client.GetGrain<IRegionGrain>(1231232, "East");

await grain.RegionGrainTask();

await host.StopAsync();