namespace ChoreographyKata;

public interface IListener
{
    Task OnMessageAsync(TheaterEvent theaterEvent);
}