using Azure.Messaging;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace ChoreographyKata.Functions.Functions;

public sealed class TicketingFunction
{
    private readonly ILogger<TicketingFunction> _logger;
    private readonly TicketingService _ticketingService;

    public TicketingFunction(ILogger<TicketingFunction> logger, TicketingService ticketingService)
    {
        _logger = logger;
        _ticketingService = ticketingService;
    }

    [Function(nameof(PrintTicket))]
    public void PrintTicket([EventGridTrigger] CloudEvent cloudEvent)
    {
        _logger.LogInformation("{functionName} called on event type {eventType}, subject {eventSubject}.",
            nameof(PrintTicket),
            cloudEvent.Type,
            cloudEvent.Subject);

        if (cloudEvent.Data == null)
        {
            return;
        }

        _ticketingService.OnMessage(cloudEvent.Data.ToObjectFromJson<TheaterEvent>());
    }
}