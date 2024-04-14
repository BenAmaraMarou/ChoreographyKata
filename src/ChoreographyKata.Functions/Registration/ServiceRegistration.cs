using ChoreographyKata.Broker;
using ChoreographyKata.Broker.Configuration;
using ChoreographyKata.Calendar;
using ChoreographyKata.ControlTower;
using ChoreographyKata.ControlTower.Configuration;
using ChoreographyKata.ControlTower.InspectedTheaterEvents;
using ChoreographyKata.CorrelationId;
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
        services.AddTransient<ICalendar, LocalCalendar>();
        services.AddTransient<ICorrelationIdFactory, CorrelationIdFactory>();
        services.AddConfiguration<EventGridConfiguration>(EventGridConfiguration.SectionKey);
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
        services.AddTransient<ITheaterEvents, InMemoryTheaterEvents>();
        services.AddConfiguration<ControlTowerConfiguration>(ControlTowerConfiguration.SectionKey);
        services.AddTransient<ValidationRule>();
        services.AddTransient<ControlTowerService>();
    }
}