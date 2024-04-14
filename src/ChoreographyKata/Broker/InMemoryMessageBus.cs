namespace ChoreographyKata.Broker;

public sealed record InMemoryMessageBus : IMessageBus
{
    private readonly List<IListener> _listeners = new();

    public void Subscribe(IListener listener)
    {
        _listeners.Add(listener);
    }

    public Task SendAsync(TheaterEvent theaterEvent)
    {
        foreach (var listener in _listeners)
        {
            listener.OnMessage(theaterEvent);
        }

        return Task.CompletedTask;
    }
}