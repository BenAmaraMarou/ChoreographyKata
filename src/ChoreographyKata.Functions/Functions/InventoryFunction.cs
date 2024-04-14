// Default URL for triggering event grid function in the local environment.
// http://localhost:7071/runtime/webhooks/EventGrid?functionName={functionname}
using Azure.Messaging;
using Microsoft.Azure.Functions.Worker;

namespace ChoreographyKata.Functions.Functions;

public sealed class InventoryFunction
{
    private readonly InventoryService _inventoryService;

    public InventoryFunction(InventoryService inventoryService)
    {
        _inventoryService = inventoryService;
    }

    [Function(nameof(DecreaseCapacity))]
    public async Task DecreaseCapacity([EventGridTrigger] CloudEvent cloudEvent)
    {
        await _inventoryService.OnMessageAsync(cloudEvent.Data!.ToObjectFromJson<TheaterEvent>());
    }
}