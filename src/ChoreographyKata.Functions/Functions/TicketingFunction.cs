using Azure.Messaging;
using Microsoft.Azure.Functions.Worker;

namespace ChoreographyKata.Functions.Functions;

public sealed class TicketingFunction
{
    private readonly TicketingService _ticketingService;

    public TicketingFunction(TicketingService ticketingService)
    {
        _ticketingService = ticketingService;
    }

    [Function(nameof(PrintTicket))]
    public async Task PrintTicket([EventGridTrigger] CloudEvent cloudEvent)
    {
        var domainEvent = cloudEvent.Data!.ToObjectFromJson<DomainEvent>();

        await _ticketingService.OnMessageAsync(domainEvent);
    }
}