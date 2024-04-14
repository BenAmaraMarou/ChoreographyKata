namespace ChoreographyKata.CorrelationId;

public interface ICorrelationIdFactory
{
    Guid New();
}