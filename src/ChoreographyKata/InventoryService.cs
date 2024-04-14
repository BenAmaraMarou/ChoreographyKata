using ChoreographyKata.Broker;
using ChoreographyKata.Logging;

namespace ChoreographyKata;

public sealed record InventoryService : IListener
{
    private readonly IMessageBus _messageBus;
    private readonly ILogging _logging;
    private int _capacity;

    public InventoryService(IMessageBus messageBus, ILogging logging, int capacity)
    {
        _messageBus = messageBus;
        _logging = logging;
        _capacity = capacity;
    }

    public int AvailableSeats() => _capacity;

    public void OnMessage(TheaterEvent theaterEvent)
    {
        if (theaterEvent.Name != TheaterEvents.BookingReserved)
        {
            return;
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