using Orleans.Streams;

namespace OrleansStreamingAdvanced;

public class StreamConsumerGrain : Grain, IStreamConsumerGrain
{
    private readonly ConsumptionReport _report = new();
    
    private IAsyncObservable<string> _consumer;
    private int _numConsumedItems;
    private StreamSubscriptionHandle<string> consumerHandle;

    public Task<ConsumptionReport> GetConsumptionReport() => Task.FromResult(_report);
    
    // Activate the subscriber grain
    public override Task OnActivateAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine( "Subscriber OnActivateAsync" );
        _numConsumedItems = 0;
        consumerHandle = null;
        return Task.CompletedTask;
    }

    // We are not using implicit subscription so we register some delegates to register with the producer stream
    public async Task SubscribeToStream(Guid streamId, string streamNamespace, string providerToUse)
    {
        Console.WriteLine( "Subscribing delegates to message stream" );
        IStreamProvider streamProvider = this.GetStreamProvider( providerToUse );
        _consumer = streamProvider.GetStream<string>(streamNamespace, streamId);
        //We pass delegates to the methods to be hooked
        consumerHandle = await _consumer.SubscribeAsync( onNextAsync:  OnNextAsync, onErrorAsync: OnErrorAsync, onCompletedAsync: OnCompletedAsync );
    }

    public async Task UnsubscribeFromStream()
    {
        Console.WriteLine( "Unsubscribe from stream" );
        if ( consumerHandle != null )
        {
            await consumerHandle.UnsubscribeAsync();
        }
    }

    public Task<int> GetNumberConsumed()
    {
        return Task.FromResult( _numConsumedItems );
    }

    public Task OnNextAsync( string item, StreamSequenceToken? token)
    {
        Console.WriteLine( $"OnNextAsync( Received : {item} Token : {token}");
        _numConsumedItems++;
        return Task.CompletedTask;
    }
    
    public Task OnCompletedAsync()
    {
        Console.WriteLine( "Subscriber OnCompletedAsync()" );
        return Task.CompletedTask;
    }

    public Task OnErrorAsync( Exception ex )
    {
        Console.WriteLine($"OnErrorAsync() {ex}");
        return Task.CompletedTask;
    }
    
    // Deactivate the subscriber grain
    public override Task OnDeactivateAsync(DeactivationReason reason, CancellationToken cancellationToken)
    {
        Console.WriteLine("Subscriber OnDeactivateAsync");
        return Task.CompletedTask;
    }
}