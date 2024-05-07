using Microsoft.Extensions.Logging;
using Orleans.Utilities;

namespace Notifications;

public class LeaseGrain : Grain, ILeaseGrain
{

    private readonly ObserverManager<IMyNotifications> _leaseObserverManager;
    
    
    /*
     * Instantiate the ObserverManager. Normally this would be injected
     *
     * The TimeSpan represents a time period after which observers are lazily removed
     */
    public LeaseGrain()
    {
        var logger = LoggerFactory
            .Create(logging => logging.AddConsole())
            .CreateLogger<ILeaseGrain>();
        _leaseObserverManager = new ObserverManager<IMyNotifications>(TimeSpan.FromMinutes(1), logger);
    }

    /*
     * Register our observer with the ObserverManager class which manages subscriptions for us. This class
     * has been included since Orleans 7 to streamline the notifications process
     */
    public Task Subscribe(IMyNotifications observer)
    {
        _leaseObserverManager.Subscribe(observer, observer);

        return Task.CompletedTask;
    }

    public Task UnSubscribe(IMyNotifications observer)
    {
        _leaseObserverManager.Unsubscribe(observer);

        return Task.CompletedTask;
    }


    public Task SendNotificationMessage(string message)
    {
        _leaseObserverManager.Notify(s => s.ProcessNotification(message));

        return Task.CompletedTask;
    }
}