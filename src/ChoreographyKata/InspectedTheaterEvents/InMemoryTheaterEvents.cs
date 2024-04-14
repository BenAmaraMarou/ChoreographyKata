namespace ChoreographyKata.InspectedTheaterEvents;

public sealed record InMemoryTheaterEvents : ITheaterEvents
{
    private readonly Dictionary<TheaterEvent, DateTime> _events = new();

    public void Add(TheaterEvent theaterEvent, DateTime date)
    {
        _events.TryAdd(theaterEvent, date);
    }

    public IEnumerable<TheaterEvent> Get() => _events.Keys;
}