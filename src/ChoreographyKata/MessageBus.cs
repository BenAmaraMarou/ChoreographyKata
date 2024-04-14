namespace ChoreographyKata;

public record MessageBus
{
    private readonly List<IListener> _listeners = new();

    public void Subscribe(IListener listener)
    {
        _listeners.Add(listener);
    }

    public void Send(TheaterEvent theaterEvent)
    {
        foreach (var listener in _listeners)
        {
            listener.OnMessage(theaterEvent);
        }
    }
}