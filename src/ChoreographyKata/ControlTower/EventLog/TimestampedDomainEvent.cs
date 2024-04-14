namespace ChoreographyKata.ControlTower.EventLog;

public sealed record TimestampedDomainEvent(Guid CorrelationId, string Name, int Value, DateTime Date) 
    : DomainEvent(CorrelationId, Name, Value)
{
    public override string ToString() => $"{this} {Date}";
}