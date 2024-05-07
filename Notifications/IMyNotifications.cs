namespace Notifications;

public interface IMyNotifications : IGrainObserver
{
    Task ProcessNotification(string notification);

}