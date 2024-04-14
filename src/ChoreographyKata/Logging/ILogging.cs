namespace ChoreographyKata.Logging;

public interface ILogging
{
    void Log(TheaterEvent theaterEvent);

    void Log(string message);
}