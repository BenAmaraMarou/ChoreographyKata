using ChoreographyKata.Broker;

namespace ChoreographyKata;

public sealed record InventoryService : IListener
{
    private const int DefaultCapacity = 10;
    private int _capacity;
    private readonly IMessageBus _messageBus;
    private readonly ILogger _logger;

    public InventoryService(IMessageBus messageBus, ILogger logger) 
        : this(DefaultCapacity, messageBus, logger)
    { }

    public InventoryService(int capacity, IMessageBus messageBus, ILogger logger)
    {
        _capacity = capacity;
        _messageBus = messageBus;
        _logger = logger;
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
            var capacityExceeded = theaterEvent with { Name = TheaterEvents.CapacityExceeded };
            _messageBus.SendAsync(capacityExceeded);
            _logger.Log(capacityExceeded);
        }
        else
        {
            DecrementCapacity(theaterEvent.Value);
        }
    }

    private void DecrementCapacity(int numberOfSeats)
    {
        _capacity -= numberOfSeats;

        var capacityReserved = new TheaterEvent(TheaterEvents.CapacityReserved, numberOfSeats);
        _messageBus.SendAsync(capacityReserved);
        _logger.Log(capacityReserved);
    }
}