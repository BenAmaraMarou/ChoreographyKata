namespace ChoreographyKata;

public interface IListener
{
    Task OnMessage(TheaterEvent theaterEvent);
}