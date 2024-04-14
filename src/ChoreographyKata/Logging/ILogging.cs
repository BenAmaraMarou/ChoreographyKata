namespace ChoreographyKata.Logging;

public interface ILogging
{
    void Log(DomainEvent domainEvent);

    void Log(string message);
}