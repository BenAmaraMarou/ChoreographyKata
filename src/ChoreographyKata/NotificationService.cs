namespace ChoreographyKata;

public sealed class NotificationService : IListener
{
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