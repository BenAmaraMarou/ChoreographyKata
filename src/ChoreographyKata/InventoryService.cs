namespace ChoreographyKata;

public sealed record InventoryService
{
    private int _capacity;

    public InventoryService(int capacity)
    {
        _capacity = capacity;
    }

    public bool DecrementCapacity(int numberOfSeats)
    {
        if (_capacity < numberOfSeats)
        {
            Console.WriteLine($"CapacityExceeded {numberOfSeats}");
            return false;
        }

        _capacity-=numberOfSeats;
        Console.WriteLine($"CapacityReserved {numberOfSeats}");
        return true;
    }

    public int AvailableSeats()
    {
        return _capacity;
    }
}