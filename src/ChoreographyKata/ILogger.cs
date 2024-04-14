namespace ChoreographyKata;

public interface ILogger
{
    void Log(TheaterEvent theaterEvent);

    void Log(string message);
}