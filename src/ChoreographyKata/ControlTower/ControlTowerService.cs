using ChoreographyKata.Calendar;
using ChoreographyKata.EventLogs;
using ChoreographyKata.Logging;

namespace ChoreographyKata.ControlTower;

public sealed class ControlTowerService
{
    private readonly IEventLog _eventLog;
    private readonly ICalendar _calendar;
    private readonly ILogging _logging;
    private readonly ValidationRule _validationRule;

    public ControlTowerService(IEventLog eventLog,
        ICalendar calendar,
        ILogging logging, 
        ValidationRule validationRule)
    {
        _eventLog = eventLog;
        _calendar = calendar;
        _logging = logging;
        _validationRule = validationRule;
    }

    public async Task InspectErrorsAsync()
    {
        var koCorrelationIds = await GetKoCorrelationIdsAsync();

        _logging.Log($"KO Correlation Ids: [{string.Join(", ", koCorrelationIds)}");
    }

    public async Task<IEnumerable<Guid>> GetKoCorrelationIdsAsync()
    {
        var now = _calendar.Now();
        
        //TODO limit ObservationPeriod to past 2 days (for instance)

        return (await _eventLog.GetAsync())
            .GroupBy(t => t.CorrelationId)
            .Where(eventsByCorrelationId => !_validationRule.AreValid(eventsByCorrelationId.ToList(), now))
            .Select(eventsByCorrelationId => eventsByCorrelationId.Key);
    }
}