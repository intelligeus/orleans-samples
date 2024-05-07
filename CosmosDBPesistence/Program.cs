// See https://aka.ms/new-console-template for more information

using CommonGrains;
using CommonGrains.Interfaces;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Orleans;
using Orleans.Configuration;
using Orleans.Hosting;


using var host = new HostBuilder()
    .UseOrleans(builder =>
    {
        builder.UseLocalhostClustering();
        // Use the CosmosDB emulator
        builder.AddCosmosGrainStorage("cosmosdb",
            configureOptions: static options =>
            {
                options.IsResourceCreationEnabled = true;
                options.DatabaseName = "Orleans-Samples";
                options.ContainerName = "TelematicDeviceContainer";
                options.ContainerThroughputProperties = ThroughputProperties.CreateAutoscaleThroughput(1000);
                options.ConfigureCosmosClient("AccountEndpoint=https://localhost:8081/;AccountKey=C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==;DisableServerCertificateValidation=true");
            });
        
        // Force some values for cleanup in the silo
        builder.Configure<GrainCollectionOptions>(options =>
        {
            options.CollectionQuantum = TimeSpan.FromSeconds(5); 
            options.CollectionAge = TimeSpan.FromSeconds(6); // This value must be greater than CollectionQuantum
        }); 
    })
    .Build();


// Start the host
await host.StartAsync();

// Orleans handles the lifecycle of our grain meaning we need a service to access it
var grainFactory = host.Services.GetRequiredService<IGrainFactory>();

// Get a reference to the grain
var telematicGrain = grainFactory.GetGrain<ITelematicDeviceGrain>(4269867);

await telematicGrain.ReceiveMessage("Message 0");
await telematicGrain.GenerateMessage();

Thread.Sleep(1000);

await telematicGrain.ReceiveMessage("Message 1");
await telematicGrain.GenerateMessage();

Thread.Sleep(1000);

await telematicGrain.ReceiveMessage("Message 2");
await telematicGrain.GenerateMessage();
Thread.Sleep(20000);

await telematicGrain.TraceMessages();

Console.WriteLine("Orleans is running.\nPress Enter to terminate...");
Console.ReadLine();

Console.WriteLine("Orleans is stopping...");

await host.StopAsync();