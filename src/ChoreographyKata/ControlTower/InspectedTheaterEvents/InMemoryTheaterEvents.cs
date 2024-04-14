namespace ChoreographyKata.ControlTower.InspectedTheaterEvents;

public sealed record InMemoryTheaterEvents : ITheaterEvents
{
    private readonly Dictionary<TheaterEvent, DateTime> _events = new();

    public Task AddAsync(TheaterEvent theaterEvent, DateTime dateTime) => 
       Task.FromResult(_events.TryAdd(theaterEvent, dateTime));

    public Task<IReadOnlyDictionary<TheaterEvent, DateTime>> GetAsync() => Task.FromResult<IReadOnlyDictionary<TheaterEvent, DateTime>>(_events);
}