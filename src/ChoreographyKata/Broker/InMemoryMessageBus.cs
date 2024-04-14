namespace ChoreographyKata.Broker;

public sealed record InMemoryMessageBus : IMessageBus
{
    private readonly List<IListener> _listeners = new();

    public void Subscribe(IListener listener)
    {
        _listeners.Add(listener);
    }

    public async Task SendAsync(TheaterEvent theaterEvent)
    {
        foreach (var listener in _listeners)
        {
            await listener.OnMessageAsync(theaterEvent);
        }
    }
}