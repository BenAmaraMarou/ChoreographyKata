using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ChoreographyKata.Functions.Registration;

public static class FunctionServices
{
    public static void Register(IServiceCollection services)
    {
        services.AddSingleton<MessageBus>();
        services.AddTransient<BookingService>();
        services.AddSingleton<IListener, InventoryService>();
        services.AddTransient<IListener, TicketingService>();
        services.AddTransient<IListener, NotificationService>();

        var provider = services.BuildServiceProvider();
        var bus = provider.GetService<MessageBus>()!;
        var listeners = provider.GetServices<IListener>();
        foreach (var listener in listeners)
        {
            bus.Subscribe(listener);
        }

        services.Replace(ServiceDescriptor.Singleton(bus));
    }
}