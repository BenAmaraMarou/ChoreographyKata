namespace ChoreographyKata;

public sealed class NotificationService : IListener
{
    private readonly ILogger _logger;

    public NotificationService(ILogger logger)
    {
        _logger = logger;
    }

    public void OnMessage(TheaterEvent theaterEvent)
    {
        if (theaterEvent.Name != TheaterEvents.CapacityExceeded)
        {
            return;
        }

        NotifyFailure(theaterEvent.Value);
    }

    private void NotifyFailure(int numberOfSeats)
    {
        _logger.Log($"FailureNotified {numberOfSeats}");
    }
}