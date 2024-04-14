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

    [Function(nameof(ReserveInventory))]
    public async Task ReserveInventory([EventGridTrigger] CloudEvent cloudEvent)
    {
        var domainEvent = cloudEvent.Data!.ToObjectFromJson<DomainEvent>();

        await _inventoryService.OnMessageAsync(domainEvent);
    }
}