namespace ChoreographyKata.InspectedTheaterEvents;

public interface ITheaterEvents
{
    void Add(TheaterEvent theaterEvent, DateTime dateTime);

    IEnumerable<TheaterEvent> Get();
}