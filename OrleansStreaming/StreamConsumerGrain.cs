using Orleans.Streams;
using Orleans.Streams.Core;

namespace OrleansStreaming;

// Identify the  Stream Namespace we wish to subscribe to because we have Implicit subscriptions active
[ImplicitStreamSubscription("StreamNamespace")]
public class StreamConsumerGrain : Grain, IStreamConsumerGrain, IStreamSubscriptionObserver, IAsyncObserver<string>
{
    private readonly ConsumptionReport _report = new();
    
    private IAsyncObservable<string> _consumer;
    private int _numConsumedItems;
    private StreamSubscriptionHandle<string> consumerHandle;

    public Task<ConsumptionReport> GetConsumptionReport() => Task.FromResult(_report);

    // Register this class to with the producer stream
    public Task OnSubscribed(IStreamSubscriptionHandleFactory handleFactory)
    {
        StreamSubscriptionHandle<string> handle = handleFactory.Create<string>();
        
        return handle.ResumeAsync(this);
    }
    
    public override Task OnActivateAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine( "Subscriber OnActivateAsync" );
        _numConsumedItems = 0;
        consumerHandle = null;
        return Task.CompletedTask;
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