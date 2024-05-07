namespace Notifications;

public class LeasingClient : IMyNotifications
{
    public Task ProcessNotification(string notification)
    {
       Console.WriteLine($"Client notification received @ {DateTime.Now:T} - {notification}");
       
       return Task.CompletedTask;
    }
}