namespace ChoreographyKata.EventLogs;

public sealed record TimestampedDomainEvent(Guid CorrelationId, string Name, int Value, DateTime Date)
    : DomainEvent(CorrelationId, Name, Value)
{
    public override string ToString() => $"CAPTURED: {base.ToString()} {Date}";
}