using ChoreographyKata.Broker;

namespace ChoreographyKata;

public sealed record InventoryService : IListener
{
    private const int DefaultCapacity = 10;
    private int _capacity;
    private readonly IMessageBus _messageBus;

    public InventoryService(IMessageBus messageBus) 
        : this(DefaultCapacity, messageBus)
    { }

    public InventoryService(int capacity, IMessageBus messageBus)
    {
        _capacity = capacity;
        _messageBus = messageBus;
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
            _messageBus.SendAsync(theaterEvent with { Name = TheaterEvents.CapacityExceeded });
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
        _messageBus.SendAsync(new TheaterEvent(TheaterEvents.CapacityReserved, numberOfSeats));
    }
}