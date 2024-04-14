using ChoreographyKata.Broker;
using ChoreographyKata.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ChoreographyKata.Functions.Registration;

public static class ServiceRegistration
{
    private const string InventoryConfigSection = "InventoryCapacity";

    public static void Register(IServiceCollection services)
    {
        services.AddTransient<ILogging, TheaterLogger>();
        services.AddSingleton<IMessageBus, EventGrid>();
        services.AddTransient<BookingService>();
        services.AddSingleton<IListener>(p =>
        {
            var capacity = p.GetRequiredService<IConfiguration>().GetValue<int>(InventoryConfigSection);
            var bus = p.GetRequiredService<IMessageBus>();
            var logger = p.GetRequiredService<ILogging>();

            return new InventoryService(bus, logger, capacity);
        });
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