using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Reminders;

// Configure the host using localhost clustering. This is where our grain will execute 
// For the reminder service we are using the InMemoryReminderService
using var host = new HostBuilder()
    .UseOrleans(builder => builder.UseLocalhostClustering()
                                    .UseInMemoryReminderService())
    .Build();

// Start the host
await host.StartAsync();

// Orleans handles the lifecycle of our grain so we need a service to access it
var grainFactory = host.Services.GetRequiredService<IGrainFactory>();

// Get a reference to the reminder grain
var reminder = grainFactory.GetGrain<IReminder>("1");

// Call the grain and let the reminders execute
await reminder.ActivateMe();

// Shut down the host
Console.WriteLine("Orleans is running.\nPress Enter to terminate...");
Console.ReadLine();
Console.WriteLine("Orleans is stopping...");

await host.StopAsync();