namespace Notifications;

public interface ILeaseGrain : IGrainWithStringKey
{
    Task Subscribe(IMyNotifications observer);
    Task UnSubscribe(IMyNotifications observer);

    Task SendNotificationMessage(string message);
}