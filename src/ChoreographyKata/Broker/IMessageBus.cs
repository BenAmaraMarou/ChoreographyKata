namespace ChoreographyKata.Broker;

public interface IMessageBus
{
    void Subscribe(IListener listener);

    Task SendAsync(TheaterEvent theaterEvent);
}