// Default URL for triggering event grid function in the local environment.
// http://localhost:7071/runtime/webhooks/EventGrid?functionName={functionname}

using Azure.Messaging;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace ChoreographyKata.Functions.Functions;

public sealed class InventoryFunction
{
    private readonly ILogger<InventoryFunction> _logger;
    private readonly InventoryService _inventoryService;

    public InventoryFunction(ILogger<InventoryFunction> logger, InventoryService inventoryService)
    {
        _logger = logger;
        _inventoryService = inventoryService;
    }

    [Function(nameof(DecreaseCapacity))]
    public void DecreaseCapacity([EventGridTrigger] CloudEvent cloudEvent)
    {
        _logger.LogInformation("{functionName} called on event type {eventType}, subject {eventSubject}.",
            nameof(DecreaseCapacity),
            cloudEvent.Type,
            cloudEvent.Subject);

        if (cloudEvent.Data == null)
        {
            return;
        }

        _inventoryService.OnMessage(cloudEvent.Data.ToObjectFromJson<TheaterEvent>());
    }
}