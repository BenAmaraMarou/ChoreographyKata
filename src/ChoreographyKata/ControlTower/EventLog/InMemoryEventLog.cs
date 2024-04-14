namespace ChoreographyKata.ControlTower.EventLog;

public sealed record InMemoryEventLog : IEventLog
{
    private readonly HashSet<TimestampedDomainEvent> _events = new();

    public Task AppendAsync(TimestampedDomainEvent domainEvent) =>
        Task.FromResult(_events.Add(domainEvent));

    public Task<IReadOnlyCollection<TimestampedDomainEvent>> GetAsync() => 
        Task.FromResult<IReadOnlyCollection<TimestampedDomainEvent>>(_events);
}