namespace ChoreographyKata;

public sealed class TicketingService : IListener
{
    private readonly MessageBus _messageBus;

    public TicketingService(MessageBus messageBus)
    {
        _messageBus = messageBus;
        _messageBus.Subscribe(this);
    }

    public void OnMessage(TheaterEvent theaterEvent)
    {
        if (theaterEvent.Name != TheaterEvents.CapacityReserved)
        {
            return;
        }

        PrintTicket(theaterEvent.Value);
    }

    private static void PrintTicket(int numberOfSeats)
    {
        Console.WriteLine($"TicketPrinted {numberOfSeats}");
    }
}