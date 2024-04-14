using ChoreographyKata.Broker;
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

    public Task OnMessageAsync(TheaterEvent theaterEvent)
    {
        if (theaterEvent.Name != TheaterEvents.BookingReserved)
        {
            return Task.CompletedTask;
        }

        if (_capacity < theaterEvent.Value)
        {
            CapacityExceeded(theaterEvent);
        }
        else
        {
            DecrementCapacity(theaterEvent.Value);
            CapacityReserved(theaterEvent);
        }

        return Task.CompletedTask;
    }

    private void DecrementCapacity(int numberOfSeats) => _capacity -= numberOfSeats;

    private void CapacityReserved(TheaterEvent theaterEvent)
    {
        var capacityReserved = theaterEvent with{ Name = TheaterEvents.CapacityReserved};
        _messageBus.SendAsync(capacityReserved);
        _logging.Log(capacityReserved);
    }

    private void CapacityExceeded(TheaterEvent theaterEvent)
    {
        var capacityExceeded = theaterEvent with { Name = TheaterEvents.CapacityExceeded };
        _messageBus.SendAsync(capacityExceeded);
        _logging.Log(capacityExceeded);
    }
}