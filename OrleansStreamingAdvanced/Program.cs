// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Orleans.Providers;
using Orleans.Streams;
using OrleansStreamingAdvanced;


using var host = new HostBuilder()
    .UseOrleans(builder =>
    {
        builder.UseLocalhostClustering();
        // Use the CosmosDB emulator
        
        builder.AddMemoryGrainStorage("PubSubStore")
            .AddMemoryStreams<DefaultMemoryMessageBodySerializer>("stream-test", mem =>
            {
                mem.ConfigurePartitioning(1); // We only want one queue running
                mem.ConfigurePullingAgent(opt => opt.Configure(options => options.BatchContainerBatchSize = 10));
                mem.ConfigureStreamPubSub(StreamPubSubType.ExplicitGrainBasedAndImplicit);
            });
    })
    .Build();


// Start the host
await host.StartAsync();

// Orleans handles the lifecycle of our grain meaning we need a service to access it
var grainFactory = host.Services.GetRequiredService<IGrainFactory>();

var producer = grainFactory.GetGrain<ITestProducerGrain>(Guid.NewGuid());

// Get a reference to the Subscriber grain which automatically registers with the stream 
var consumer = grainFactory.GetGrain<IStreamConsumerGrain>(Guid.NewGuid());
await consumer.SubscribeToStream(new Guid(), "StreamNamespace", "stream-test");

await producer.RegisterAsProducer(new Guid(), "StreamNamespace", "stream-test");

// Start inserting data into the stream
await producer.StartPeriodicProducing();

// This runs pretty quickly so shut down after some data has been produced
await Task.Delay(TimeSpan.FromMilliseconds(5000));

await producer.StopPeriodicProducing();

Console.WriteLine("Orleans is running.\nPress Enter to terminate...");
Console.ReadLine();

Console.WriteLine("Orleans is stopping...");

await host.StopAsync();