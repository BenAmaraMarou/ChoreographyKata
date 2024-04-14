using Azure.Messaging;
using ChoreographyKata.Logging;
using Microsoft.Azure.Functions.Worker;

namespace ChoreographyKata.Functions.Functions;

public sealed class TicketingFunction
{
    private readonly ILogging _logger;
    private readonly TicketingService _ticketingService;

    public TicketingFunction(ILogging logger, TicketingService ticketingService)
    {
        _logger = logger;
        _ticketingService = ticketingService;
    }

    [Function(nameof(PrintTicket))]
    public async Task PrintTicket([EventGridTrigger] CloudEvent cloudEvent)
    {
        _logger.Log($"{nameof(PrintTicket)} called on event type {cloudEvent.Type}, subject {cloudEvent.Subject}.");

        if (cloudEvent.Data == null)
        {
            return;
        }

        await _ticketingService.OnMessageAsync(cloudEvent.Data.ToObjectFromJson<TheaterEvent>());
    }
}