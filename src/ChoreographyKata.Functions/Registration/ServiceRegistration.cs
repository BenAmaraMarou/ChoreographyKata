using ChoreographyKata.Broker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ChoreographyKata.Functions.Registration;

public static class ServiceRegistration
{
    public static void Register(IServiceCollection services)
    {
        services.AddTransient<ILogger, TheaterLogger>();
        services.AddSingleton<IMessageBus, EventGrid>();
        services.AddTransient<BookingService>();
        services.AddSingleton<IListener, InventoryService>();
        services.AddTransient<IListener, TicketingService>();
        services.AddTransient<IListener, NotificationService>();
        services
            .AddOptions<EventGridConfiguration>()
            .Configure<IConfiguration>((configuration, config) =>
            {
                config.GetSection(EventGridConfiguration.SectionKey).Bind(configuration);
            });
    }
}