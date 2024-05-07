using Microsoft.Extensions.Hosting;

new HostBuilder()
    .UseOrleans(o => o.UseDashboard(options => { }))
        .Build();