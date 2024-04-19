using ChoreographyKata.Broker;
using ChoreographyKata.Logging;

namespace ChoreographyKata;

public sealed class NotificationService : IListener
{
    private readonly ILogging _logging;
    private readonly IMessageBus _messageBus;

    public NotificationService(ILogging logging, IMessageBus messageBus)
    {
        _logging = logging;
        _messageBus = messageBus;
    }

    public Task OnMessageAsync(DomainEvent domainEvent)
    {
        if (domainEvent.Name == DomainEventCatalog.CapacityExceeded)
        {
            NotifyFailure(domainEvent);
        }

        return Task.CompletedTask;
    }

    private void NotifyFailure(DomainEvent domainEvent)
    {
        var notificationSent = domainEvent with { Name = DomainEventCatalog.NotificationSent };
        _logging.Log(notificationSent);
        _messageBus.Publish(notificationSent);
    }
}