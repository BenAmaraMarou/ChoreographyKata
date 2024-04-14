namespace ChoreographyKata;

public sealed class TicketingService : IListener
{
    private readonly ILogger _logger;

    public TicketingService(ILogger logger)
    {
        _logger = logger;
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
        _logger.Log($"TicketPrinted {numberOfSeats}");
    }
}