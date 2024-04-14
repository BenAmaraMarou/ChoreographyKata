namespace ChoreographyKata.ControlTower.InspectedTheaterEvents;

public sealed record InMemoryTheaterEvents : ITheaterEvents
{
    private readonly Dictionary<TheaterEvent, DateTime> _events = new();

    public void Add(TheaterEvent theaterEvent, DateTime dateTime) => 
        _events.TryAdd(theaterEvent, dateTime);

    public IReadOnlyDictionary<TheaterEvent, DateTime> Get() => _events;
}