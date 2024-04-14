namespace ChoreographyKata;

public sealed class NotificationService
{
    public void NotifyFailure(int numberOfSeats)
    {
        Console.WriteLine($"Notification sent {numberOfSeats}");
    }
}