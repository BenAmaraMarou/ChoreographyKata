namespace ChoreographyKata;

public sealed class TicketingService
{
    public void PrintTicket(int numberOfSeats)
    {
        Console.WriteLine($"Tickets printed {numberOfSeats}");
    }
}