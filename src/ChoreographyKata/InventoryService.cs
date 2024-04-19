using ChoreographyKata.Broker;
using ChoreographyKata.Configuration;
using ChoreographyKata.Logging;
using Microsoft.Extensions.Options;

namespace ChoreographyKata;

public sealed record InventoryService : IListener
{
    private readonly IMessageBus _messageBus;
    private readonly ILogging _logging;
    private int _capacity;

    public InventoryService(IMessageBus messageBus, ILogging logging, IOptions<InventoryConfiguration> options)
    {
        _messageBus = messageBus;
        _logging = logging;
        _capacity = options.Value.Capacity;
    }

    public int AvailableSeats() => _capacity;

    public Task OnMessageAsync(DomainEvent domainEvent)
    {
        if (domainEvent.Name == DomainEventCatalog.BookingRequested)
        {
            ReserveInventory(domainEvent);
        }

        return Task.CompletedTask;
    }

    private void ReserveInventory(DomainEvent domainEvent)
    {
        if (_capacity >= domainEvent.Value)
        {
            DecrementCapacity(domainEvent);
            PublishInventoryReserved(domainEvent);
            PublishInventoryUpdated(domainEvent);
        }
        else
        {
            PublishCapacityExceeded(domainEvent);
        }
    }

    private void DecrementCapacity(DomainEvent domainEvent) => 
        _capacity -= domainEvent.Value;

    private void PublishInventoryReserved(DomainEvent domainEvent)
    {
        var inventoryReserved = domainEvent with { Name = DomainEventCatalog.InventoryReserved };
        _messageBus.Publish(inventoryReserved);
        _logging.Log(inventoryReserved);
    }

    private void PublishInventoryUpdated(DomainEvent domainEvent)
    {
        var inventoryUpdated = domainEvent with { Name = DomainEventCatalog.InventoryUpdated };
        _messageBus.Publish(inventoryUpdated);
        _logging.Log(inventoryUpdated);
    }
    private void PublishCapacityExceeded(DomainEvent domainEvent)
    {
        var capacityExceeded = domainEvent with { Name = DomainEventCatalog.CapacityExceeded };
        _messageBus.Publish(capacityExceeded);
        _logging.Log(capacityExceeded);
    }
}