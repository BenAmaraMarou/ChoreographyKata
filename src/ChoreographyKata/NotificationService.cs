using ChoreographyKata.Logging;

namespace ChoreographyKata;

public sealed class NotificationService : IListener
{
    private readonly ILogging _logging;

    public NotificationService(ILogging logging)
    {
        _logging = logging;
    }

    public Task OnMessageAsync(DomainEvent domainEvent)
    {
        if (domainEvent.Name == DomainEventCatalog.CapacityExceeded)
        {
            NotifyFailure(domainEvent.Value);
        }

        return Task.CompletedTask;
    }

    private void NotifyFailure(int numberOfSeats)
    {
        _logging.Log($"{DomainEventCatalog.NotificationSent} {numberOfSeats}");
    }
}