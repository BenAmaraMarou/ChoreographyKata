namespace ChoreographyKata;

public sealed record InventoryService : IListener
{
    private int _capacity;
    private readonly MessageBus _messageBus;

    public InventoryService(int capacity, MessageBus messageBus)
    {
        _capacity = capacity;
        _messageBus = messageBus;
        _messageBus.Subscribe(this);
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
            Console.WriteLine($"{TheaterEvents.CapacityExceeded} {theaterEvent.Value}");
            _messageBus.Send(theaterEvent with { Name = TheaterEvents.CapacityExceeded });
        }
        else
        {
            DecrementCapacity(theaterEvent.Value);
        }
    }

    private void DecrementCapacity(int numberOfSeats)
    {
        _capacity -= numberOfSeats;
        Console.WriteLine($"{TheaterEvents.CapacityReserved} {numberOfSeats}");
        _messageBus.Send(new TheaterEvent(TheaterEvents.CapacityReserved, numberOfSeats));
    }
}