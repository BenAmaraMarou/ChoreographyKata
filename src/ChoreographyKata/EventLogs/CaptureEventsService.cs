using ChoreographyKata.Calendar;
using ChoreographyKata.Logging;

namespace ChoreographyKata.EventLogs;

public sealed class CaptureEventsService : IListener
{
    private readonly IEventLog _eventLog;
    private readonly ICalendar _calendar;
    private readonly ILogging _logging;

    public CaptureEventsService(IEventLog eventLog,
        ICalendar calendar,
        ILogging logging)
    {
        _eventLog = eventLog;
        _calendar = calendar;
        _logging = logging;
    }

    public async Task OnMessageAsync(DomainEvent domainEvent)
    {
        var now = _calendar.Now();
        var timestampedDomainEvent = new TimestampedDomainEvent(domainEvent.CorrelationId,
            domainEvent.Name,
            domainEvent.Value,
            now);

        _logging.Log(timestampedDomainEvent);
        await _eventLog.AppendAsync(timestampedDomainEvent);
    }

    public async Task<IEnumerable<DomainEvent>> CapturedEventsAsync() => await _eventLog.GetAsync();
}