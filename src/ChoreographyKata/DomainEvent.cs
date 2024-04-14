namespace ChoreographyKata;

public record DomainEvent(Guid CorrelationId, string Name, int Value)
{
    public override string ToString() => $"{CorrelationId} - {Name} {Value}";
} 