namespace ChoreographyKata;

public sealed record TheaterEvent(Guid CorrelationId, string Name, int Value)
{
    public override string ToString() => $"{CorrelationId} - {Name} {Value}";
} 