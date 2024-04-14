namespace ChoreographyKata.Broker;

public interface IMessageBus
{
    Task Publish(DomainEvent domainEvent);
}