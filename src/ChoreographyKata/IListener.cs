namespace ChoreographyKata;

public interface IListener
{
    Task OnMessageAsync(DomainEvent domainEvent);
}