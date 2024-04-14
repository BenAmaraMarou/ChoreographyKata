namespace ChoreographyKata.ControlTower.InspectedTheaterEvents;

public interface ITheaterEvents
{
    void Add(TheaterEvent theaterEvent, DateTime dateTime);

    IReadOnlyDictionary<TheaterEvent, DateTime> Get();
}