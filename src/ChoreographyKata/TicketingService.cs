using ChoreographyKata.Logging;

namespace ChoreographyKata;

public sealed class TicketingService : IListener
{
    private readonly ILogging _logging;

    public TicketingService(ILogging logging)
    {
        _logging = logging;
    }

    public Task OnMessageAsync(DomainEvent domainEvent)
    {
        if (domainEvent.Name == DomainEventCatalog.InventoryReserved)
        {
            PrintTicket(domainEvent.Value);
        }

        return Task.CompletedTask;
    }

    private void PrintTicket(int numberOfSeats)
    {
        _logging.Log($"TicketPrinted {numberOfSeats}");
    }
}