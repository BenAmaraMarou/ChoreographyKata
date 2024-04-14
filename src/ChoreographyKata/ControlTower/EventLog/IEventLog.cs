namespace ChoreographyKata.ControlTower.EventLog;

public interface IEventLog
{
    Task AppendAsync(TimestampedDomainEvent domainEvent);

    Task<IReadOnlyCollection<TimestampedDomainEvent>> GetAsync();
}