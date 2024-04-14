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

    public bool DecrementCapacity(int numberOfSeats)
    {
        if (_capacity < numberOfSeats)
        {
            return false;
        }

        return true;
    }

    public int AvailableSeats() => _capacity;

    public void OnMessage(TheaterEvent theaterEvent)
    {
        if (_capacity < theaterEvent.Value)
        {
            Console.WriteLine($"CapacityExceeded {theaterEvent.Value}");
        }
        else
        {
            _capacity -= theaterEvent.Value;
            Console.WriteLine($"CapacityReserved {theaterEvent.Value}");
        }
    }
}