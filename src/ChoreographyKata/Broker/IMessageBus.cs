namespace ChoreographyKata.Broker;

public interface IMessageBus
{
    Task SendAsync(TheaterEvent theaterEvent);
}