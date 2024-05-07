// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Notifications;


using var host = new HostBuilder()
    .UseOrleans(builder => builder.UseLocalhostClustering())
        .Build();

// Start the host
await host.StartAsync();

// Orleans handles the lifecycle of our grain so we need a service to access it
var grainFactory = host.Services.GetRequiredService<IGrainFactory>();

// Get a reference to the grain
var leaseGrain = grainFactory.GetGrain<ILeaseGrain>("1");

// Create a reference to our client which will receive the notification
var leasingClient = new LeasingClient();

var observer = grainFactory.CreateObjectReference<IMyNotifications>(leasingClient);


await leaseGrain.Subscribe(observer);

Console.WriteLine("Sending message to LeaseGrain");
await leaseGrain.SendNotificationMessage("A first vehicle has been leased");

Thread.Sleep(5000);
Console.WriteLine("Sending second message to LeaseGrain");
await leaseGrain.SendNotificationMessage("A second vehicle has been leased");

Thread.Sleep(20000);
Console.WriteLine("Sending third message to LeaseGrain");
await leaseGrain.SendNotificationMessage("A third vehicle has been leased");

Thread.Sleep(30000);
Console.WriteLine("Sending fourth message to LeaseGrain");
await leaseGrain.SendNotificationMessage("A fourth vehicle has been leased");

Thread.Sleep(30000);
Console.WriteLine("Sending fifth message to LeaseGrain");
await leaseGrain.SendNotificationMessage("A fifth vehicle has been leased");


Thread.Sleep(30000);
Console.WriteLine("Sending sixth message to LeaseGrain");
await leaseGrain.SendNotificationMessage("A sixth vehicle has been leased");


Console.WriteLine("Orleans is running.\nPress Enter to terminate...");
Console.ReadLine();

Console.WriteLine("Orleans is stopping...");

await host.StopAsync();