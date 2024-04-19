namespace ChoreographyKata.Logging;

public interface ILogging
{
    void Log<TDomainEvent>(TDomainEvent domainEvent) where TDomainEvent : DomainEvent;

    void Log(string message);
}