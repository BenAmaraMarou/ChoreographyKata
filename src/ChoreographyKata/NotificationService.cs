namespace ChoreographyKata;

public sealed class NotificationService : IListener
{
    private readonly MessageBus _messageBus;

    public NotificationService(MessageBus messageBus)
    {
        _messageBus = messageBus;
        _messageBus.Subscribe(this);
    }

    public void OnMessage(TheaterEvent theaterEvent)
    {
        if (theaterEvent.Name != TheaterEvents.CapacityExceeded)
        {
            return;
        }

        NotifyFailure(theaterEvent.Value);
    }

    private static void NotifyFailure(int numberOfSeats)
    {
        Console.WriteLine($"Notification sent {numberOfSeats}");
    }
}