namespace ChoreographyKata.CorrelationId;

public sealed class CorrelationIdFactory : ICorrelationIdFactory
{
    public Guid New() => Guid.NewGuid();
}