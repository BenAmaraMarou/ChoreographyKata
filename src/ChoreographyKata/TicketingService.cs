using ChoreographyKata.Logging;

namespace ChoreographyKata;

public sealed class TicketingService : IListener
{
    private readonly ILogging _logging;

    public TicketingService(ILogging logging)
    {
        _logging = logging;
    }

    public void OnMessage(TheaterEvent theaterEvent)
    {
        if (theaterEvent.Name != TheaterEvents.CapacityReserved)
        {
            return;
        }

        PrintTicket(theaterEvent.Value);
    }

    private void PrintTicket(int numberOfSeats)
    {
        _logging.Log($"TicketPrinted {numberOfSeats}");
    }
}