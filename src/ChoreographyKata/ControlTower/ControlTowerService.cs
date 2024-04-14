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

    public ControlTowerService(ITheaterEvents theaterEvents,
        ICalendar calendar,
        ILogging logging, 
        ValidationRule validationRule)
    {
        _theaterEvents = theaterEvents;
        _calendar = calendar;
        _logging = logging;
        _validationRule = validationRule;
    }

    public async Task OnMessageAsync(TheaterEvent theaterEvent)
    {
        _logging.Log($"{nameof(ControlTowerService)} captured: {theaterEvent}.");
        await _theaterEvents.AddAsync(theaterEvent, _calendar.Now());
    }

    public async Task<IEnumerable<TheaterEvent>> CapturedEventsAsync() => (await _theaterEvents.GetAsync()).Keys;

    public async Task InspectErrorsAsync()
    {
        var koCorrelationIds = await GetKoCorrelationIdsAsync();
        _logging.Log($"KO Correlation Ids: [{string.Join(", ", koCorrelationIds)}");
    }

    public async Task<IEnumerable<Guid>> GetKoCorrelationIdsAsync() =>
        (await _theaterEvents.GetAsync())
            .GroupBy(t => t.Key.CorrelationId)
            .Where(eventsByCorrelationId => !_validationRule.AreValid(eventsByCorrelationId.ToDictionary(g => g.Key, g => g.Value), _calendar.Now()))
            .Select(eventsByCorrelationId => eventsByCorrelationId.Key);
}