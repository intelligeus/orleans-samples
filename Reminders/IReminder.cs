namespace Reminders;

public interface IReminder : IGrainWithStringKey
{
    Task ActivateMe();
    
}