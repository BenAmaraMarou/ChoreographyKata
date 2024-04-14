using ChoreographyKata.Logging;

namespace ChoreographyKata;

public sealed class NotificationService : IListener
{
    private readonly ILogging _logging;

    public NotificationService(ILogging logging)
    {
        _logging = logging;
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
        _logging.Log($"FailureNotified {numberOfSeats}");
    }
}