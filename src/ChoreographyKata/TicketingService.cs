namespace ChoreographyKata;

public sealed class TicketingService : IListener
{
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