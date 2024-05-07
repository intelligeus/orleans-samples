using System.Runtime.CompilerServices;
using Microsoft.Extensions.Logging;
using Orleans.Runtime;
using Orleans.Streams;

namespace OrleansStreaming;

public interface ITestProducerGrain : IGrainWithGuidKey
{
    Task RegisterAsProducer(Guid streamId, string streamNamespace, string providerToUse);

    Task StartPeriodicProducing();

    Task StopPeriodicProducing();

    Task<int> GetNumberProduced();

    Task ClearNumberProduced();
    Task Produce();
}

public class TestProducerGrain : Grain, ITestProducerGrain
    {
        private IAsyncStream<string> producer;
        private int numProducedItems;
        private IDisposable producerTimer;
        
        public override Task OnActivateAsync(CancellationToken cancellationToken)
        {
           Console.WriteLine("OnActivateAsync");
            numProducedItems = 0;
            return Task.CompletedTask;
        }

        public Task RegisterAsProducer(Guid streamId, string streamNamespace, string providerToUse)
        {
            Console.WriteLine("Register as publisher to stream");
            IStreamProvider streamProvider = this.GetStreamProvider(providerToUse);
            producer = streamProvider.GetStream<string>(streamNamespace, streamId);
            return Task.CompletedTask;
        }

        // Create a callback to create events to be published to the stream
        public Task StartPeriodicProducing()
        {
            Console.WriteLine("Start producing events for stream");
            producerTimer = base.RegisterTimer(TimerCallback, null, TimeSpan.Zero, TimeSpan.FromMilliseconds(10));
            return Task.CompletedTask;
        }

        public Task StopPeriodicProducing()
        {
            Console.WriteLine("Stop producing events for stream");
            producerTimer.Dispose();
            producerTimer = null;
            return Task.CompletedTask;
        }

        public Task<int> GetNumberProduced()
        {
            Console.WriteLine($"GetNumberProduced {numProducedItems}" );
            return Task.FromResult(numProducedItems);
        }

        public Task ClearNumberProduced()
        {
            numProducedItems = 0;
            return Task.CompletedTask;
        }

        public Task Produce()
        {
            return CreateMessage();
        }

        private Task TimerCallback(object state)
        {
            return CreateMessage();
        }

        // Create a simple string to be inserted into the stream
        private async Task CreateMessage([CallerMemberName] string caller = "DEFAULT-CALLER")
        {
            await producer.OnNextAsync($"Message {numProducedItems}");
            numProducedItems++;
            Console.WriteLine($"{caller} (item count={numProducedItems})");
        }

        // The producer grain is deactivated
        public override Task OnDeactivateAsync(DeactivationReason reason, CancellationToken cancellationToken)
        {
            Console.WriteLine("OnDeactivateAsync");
            return Task.CompletedTask;
        }
    }