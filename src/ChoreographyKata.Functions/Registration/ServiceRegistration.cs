using ChoreographyKata.Broker;
using ChoreographyKata.Broker.Configuration;
using ChoreographyKata.Calendar;
using ChoreographyKata.ControlTower;
using ChoreographyKata.ControlTower.Configuration;
using ChoreographyKata.ControlTower.InspectedTheaterEvents;
using ChoreographyKata.CorrelationId;
using ChoreographyKata.Database;
using ChoreographyKata.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ChoreographyKata.Functions.Registration;

public static class ServiceRegistration
{
    private const string DatabaseConnectionStringSection = "ChoreographyKataDatabase";

    public static void Register(IServiceCollection services)
    {
        RegisterDatabase(services);
        RegisterSupport(services);
        RegisterEventGrid(services);
        RegisterTheaterServices(services);
        RegisterControlTower(services);
    }
    private static void RegisterDatabase(IServiceCollection services)
    {
        services.AddDbContextFactory<ChoreographyKataDbContext>((provider, builder) =>
        {
            var configuration = provider.GetRequiredService<IConfiguration>();
            var connectionString = configuration.GetConnectionString(DatabaseConnectionStringSection);
            builder.UseSqlServer(connectionString);
        });
    }

    private static void RegisterEventGrid(IServiceCollection services)
    {
        services.AddConfiguration<EventGridConfiguration>(EventGridConfiguration.SectionKey);
        services.AddSingleton<IMessageBus, EventGrid>();
    }

    private static void RegisterSupport(IServiceCollection services)
    {
        services.AddTransient<ILogging, TheaterLogger>();
        services.AddTransient<ICalendar, LocalCalendar>();
        services.AddTransient<ICorrelationIdFactory, CorrelationIdFactory>();
    }

    private static void RegisterTheaterServices(IServiceCollection services)
    {
        services.AddTransient<BookingService>();
        services.AddSingleton<IListener, InventoryService>();
        services.AddTransient<InventoryService>();
        services.AddTransient<IListener, TicketingService>();
        services.AddTransient<TicketingService>();
        services.AddTransient<IListener, NotificationService>();
        services.AddTransient<NotificationService>();
    }

    private static void RegisterControlTower(IServiceCollection services)
    {
        services.AddTransient<ITheaterEvents, DbTheaterEvents>();
        services.AddConfiguration<ControlTowerConfiguration>(ControlTowerConfiguration.SectionKey);
        services.AddTransient<ValidationRule>();
        services.AddTransient<ControlTowerService>();
    }
}