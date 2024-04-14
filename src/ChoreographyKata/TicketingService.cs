using ChoreographyKata.Logging;

namespace ChoreographyKata;

public sealed class TicketingService : IListener
{
    private readonly ILogging _logging;

    public TicketingService(ILogging logging)
    {
        _logging = logging;
    }

    public Task OnMessage(TheaterEvent theaterEvent)
    {
        if (theaterEvent.Name != TheaterEvents.CapacityReserved)
        {
            return Task.CompletedTask;
        }

        PrintTicket(theaterEvent.Value);

        return Task.CompletedTask;
    }

    private void PrintTicket(int numberOfSeats)
    {
        _logging.Log($"TicketPrinted {numberOfSeats}");
    }
}