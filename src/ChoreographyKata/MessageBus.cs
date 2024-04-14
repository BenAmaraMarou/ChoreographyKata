namespace ChoreographyKata;

public record MessageBus
{
    private readonly List<IListener> _listeners = new();

    public void Send(TheaterEvent theaterEvent)
    {
        foreach (var listener in _listeners)
        {
            listener.OnMessage(theaterEvent);
        }
    }
}