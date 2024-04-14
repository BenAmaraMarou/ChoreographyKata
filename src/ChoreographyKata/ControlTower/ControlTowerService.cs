using ChoreographyKata.Calendar;
using ChoreographyKata.ControlTower.InspectedTheaterEvents;
using ChoreographyKata.Logging;

namespace ChoreographyKata.ControlTower;

public sealed class ControlTowerService : IListener
{
    private readonly ITheaterEvents _theaterEvents;
    private readonly ICalendar _calendar;
    private readonly ILogging _logging;
    private readonly ValidationRule _validationRule;
    private const string Separator = ", ";

    public ControlTowerService(ITheaterEvents theaterEvents,
        ICalendar calendar,
        ILogging logging, ValidationRule validationRule)
    {
        _theaterEvents = theaterEvents;
        _calendar = calendar;
        _logging = logging;
        _validationRule = validationRule;
    }

    public void OnMessage(TheaterEvent theaterEvent)
    {
        _logging.Log($"{nameof(ControlTowerService)} captured: {theaterEvent}.");
        _theaterEvents.Add(theaterEvent, _calendar.Now());
    }

    public IEnumerable<TheaterEvent> CapturedEvents() => _theaterEvents.Get().Keys;

    public void InspectErrors()
    {
        var koCorrelationIds = GetKoCorrelationIds();
        _logging.Log($"KO Correlation Ids: [{string.Join(Separator, koCorrelationIds)}");
    }

    public IEnumerable<Guid> GetKoCorrelationIds() =>
        _theaterEvents.Get()
            .GroupBy(t => t.Key.CorrelationId)
            .Where(eventsByCorrelationId => !_validationRule.AreValid(eventsByCorrelationId.ToDictionary(g => g.Key, g => g.Value), _calendar.Now()))
            .Select(eventsByCorrelationId => eventsByCorrelationId.Key);
}