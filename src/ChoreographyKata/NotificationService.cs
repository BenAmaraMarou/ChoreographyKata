using ChoreographyKata.Logging;

namespace ChoreographyKata;

public sealed class NotificationService : IListener
{
    private readonly ILogging _logging;

    public NotificationService(ILogging logging)
    {
        _logging = logging;
    }

    public Task OnMessage(TheaterEvent theaterEvent)
    {
        if (theaterEvent.Name != TheaterEvents.CapacityExceeded)
        {
            return Task.CompletedTask;
        }

        NotifyFailure(theaterEvent.Value);

        return Task.CompletedTask;
    }

    private void NotifyFailure(int numberOfSeats)
    {
        _logging.Log($"FailureNotified {numberOfSeats}");
    }
}