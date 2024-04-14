namespace ChoreographyKata.ControlTower.InspectedTheaterEvents;

public interface ITheaterEvents
{
    Task AddAsync(TheaterEvent theaterEvent, DateTime dateTime);

    Task<IReadOnlyDictionary<TheaterEvent, DateTime>> GetAsync();
}