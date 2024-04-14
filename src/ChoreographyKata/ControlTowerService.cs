using ChoreographyKata.Calendar;
using ChoreographyKata.InspectedTheaterEvents;
using ChoreographyKata.Logging;

namespace ChoreographyKata;

public sealed class ControlTowerService : IListener
{
    private readonly ITheaterEvents _theaterEvents;
    private readonly ICalendar _calendar;
    private readonly ILogging _logging;

    public ControlTowerService(ITheaterEvents theaterEvents, ICalendar calendar, ILogging logging)
    {
        _theaterEvents = theaterEvents;
        _calendar = calendar;
        _logging = logging;
    }

    public void OnMessage(TheaterEvent theaterEvent)
    {
        _logging.Log($"{nameof(ControlTowerService)} captured: {theaterEvent}.");
        _theaterEvents.Add(theaterEvent, _calendar.Now());
    }

    public IEnumerable<TheaterEvent> CapturedEvents() => _theaterEvents.Get();
}