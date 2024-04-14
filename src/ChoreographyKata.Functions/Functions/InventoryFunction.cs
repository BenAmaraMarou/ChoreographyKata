// Default URL for triggering event grid function in the local environment.
// http://localhost:7071/runtime/webhooks/EventGrid?functionName={functionname}

using Azure.Messaging;
using Microsoft.Azure.Functions.Worker;
using ChoreographyKata.Logging;


namespace ChoreographyKata.Functions.Functions;

public sealed class InventoryFunction
{
    private readonly ILogging _logger;
    private readonly InventoryService _inventoryService;

    public InventoryFunction(ILogging logger, InventoryService inventoryService)
    {
        _logger = logger;
        _inventoryService = inventoryService;
    }

    [Function(nameof(DecreaseCapacity))]
    public async Task DecreaseCapacity([EventGridTrigger] CloudEvent cloudEvent)
    {
        _logger.Log($"{nameof(DecreaseCapacity)} called on event type {cloudEvent.Type}, subject {cloudEvent.Subject}.");

        if (cloudEvent.Data == null)
        {
            return;
        }

        await _inventoryService.OnMessageAsync(cloudEvent.Data.ToObjectFromJson<TheaterEvent>());
    }
}