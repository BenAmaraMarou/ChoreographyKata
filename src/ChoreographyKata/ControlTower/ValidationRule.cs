using ChoreographyKata.ControlTower.Configuration;
using Microsoft.Extensions.Options;

namespace ChoreographyKata.ControlTower;

public sealed class ValidationRule
{
    private static readonly IEnumerable<string> SuccessfulBookingEvents = new[]
    {
        TheaterEvents.BookingReserved,
        TheaterEvents.CapacityReserved,
    };
    private static readonly IEnumerable<string> FailedBookingEvents = new[]
    {
        TheaterEvents.BookingReserved,
        TheaterEvents.CapacityExceeded
    };
    private static readonly IEnumerable<IEnumerable<string>> RequiredEventsByGroup = new[]
    {
        SuccessfulBookingEvents,
        FailedBookingEvents
    };
    private readonly TimeSpan _timeout;

    public ValidationRule(IOptions<ControlTowerConfiguration> options)
    {
        _timeout = TimeSpan.FromMinutes(options.Value.TimeoutMinutes);
    }

    internal bool AreValid(IDictionary<TheaterEvent, DateTime> theaterEvents, DateTime executionDate)
    {
        if (AreAllRequiredEventsPresent(theaterEvents.Keys))
        {
            return true;
        }

        return AreEventsBeforeTimeOut(theaterEvents.Values, executionDate);
    }

    private static bool AreAllRequiredEventsPresent(IEnumerable<TheaterEvent> theaterEvents) => 
        RequiredEventsByGroup.Any(required => AreEqual(required, theaterEvents.Select(t => t.Name)));

    private static bool AreEqual(IEnumerable<string> events1, IEnumerable<string> events2) =>
        events1.OrderBy(_=>_).SequenceEqual(events2.OrderBy(_ => _));

    private bool AreEventsBeforeTimeOut(IEnumerable<DateTime> dates, DateTime executionDate)
    {
        var timeoutDate = dates.Max().Add(_timeout);

        return timeoutDate > executionDate;
    }
}