namespace ChoreographyKata.Broker;

/**
 * A simple in-memory, observer-pattern-based single-threaded message bus for designing architecture and testing using unit tests before switching to using actual middleware
 */
public sealed record InMemoryMessageBus : IMessageBus
{
    private readonly List<IListener> _listeners = new();

    public void Subscribe(IListener listener)
    {
        _listeners.Add(listener);
    }

    public async Task Publish(DomainEvent domainEvent)
    {
        foreach (var listener in _listeners)
        {
            await listener.OnMessageAsync(domainEvent);
        }
    }
}