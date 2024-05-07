using Orleans.Runtime;

namespace Reminders;

public class ReminderGrain : Grain, IReminder, IRemindable
{

    private int _gitReminderCounter;
    
    public Task ActivateMe()
    {
        Console.WriteLine("Activate me called on reminder grain");
        return Task.CompletedTask;
    }

    
    /*
     * We need the grain to be activated in order to register the reminder callback
     *
     * The method takes
     *  A name for the reminder
     *  A timespan to indicate when the timer should start
     *  A timespan defining the execution interval for the reminder 1 minute in this case. Spans less than 1 min
     *  will throw exceptions
     *
     * For demo purposes we also create a second reminder with an interval of 2 minutes to show how multiple 
     * reminders can be registered and also unregistered
     */
    public override Task OnActivateAsync(CancellationToken token)
    {
        Console.WriteLine("Registering reminder");
        this.RegisterOrUpdateReminder("Member Berries", TimeSpan.FromSeconds(2), TimeSpan.FromMinutes(1));
        this.RegisterOrUpdateReminder("Git Overlord", TimeSpan.FromSeconds(5), TimeSpan.FromMinutes(2));

        return base.OnActivateAsync(token);

    }

    /*
     * This method is from the IRemindable interface and replaces the custom callback method passed to timers
     *
     * We get the name of the reminder and its, first, and current tick time and also it's period
     */
    public Task ReceiveReminder(string reminderName, TickStatus status)
    {

        if (reminderName == "Member Berries")
        {
            Console.WriteLine(
                $"This is your scheduled reminder @ {DateTime.Now:T} from {reminderName} with period {status.Period} to do something");
        }
        else
        {
            _gitReminderCounter++;
            Console.WriteLine(
                $"This is your {reminderName} @ {DateTime.Now:T} with period {status.Period} reminding you to commit and push regularly");

            // OK enough of the Git Overlord. Lets unregister it
            if (_gitReminderCounter == 3)
            {
                Console.WriteLine("The Git Overlord reminder is being unregistered");
                // We use the name of the reminder as it will be persisted across restarts where as the grain 
                // instance will not
                var reminder = this.GetReminder("Git Overlord");
                this.UnregisterReminder(reminder.Result);
            }
        }

        return Task.CompletedTask;
    }
}