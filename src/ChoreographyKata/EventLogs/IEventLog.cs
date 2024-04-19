namespace ChoreographyKata.EventLogs;

public interface IEventLog
{
    Task AppendAsync(TimestampedDomainEvent domainEvent);

    Task<IReadOnlyCollection<TimestampedDomainEvent>> GetAsync();
}