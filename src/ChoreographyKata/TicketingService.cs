using ChoreographyKata.Broker;
using ChoreographyKata.Logging;

namespace ChoreographyKata;

public sealed class TicketingService : IListener
{
    private readonly ILogging _logging;
    private readonly IMessageBus _messageBus;

    public TicketingService(ILogging logging, IMessageBus messageBus)
    {
        _logging = logging;
        _messageBus = messageBus;
    }

    public Task OnMessageAsync(DomainEvent domainEvent)
    {
        if (domainEvent.Name == DomainEventCatalog.InventoryReserved)
        {
            PrintTicket(domainEvent);
        }

        return Task.CompletedTask;
    }

    private void PrintTicket(DomainEvent domainEvent)
    {
        var ticketPrinted = domainEvent with { Name = DomainEventCatalog.TicketPrinted };
        _logging.Log(ticketPrinted);
        _messageBus.Publish(ticketPrinted);
    }
}