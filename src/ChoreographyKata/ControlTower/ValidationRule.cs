using ChoreographyKata.ControlTower.Configuration;
using ChoreographyKata.EventLogs;
using Microsoft.Extensions.Options;

namespace ChoreographyKata.ControlTower;

public sealed class ValidationRule
{
    private static readonly IEnumerable<string> SuccessfulBookingEvents = new[]
    {
        DomainEventCatalog.BookingRequested,
        DomainEventCatalog.InventoryReserved,
    };
    private static readonly IEnumerable<string> FailedBookingEvents = new[]
    {
        DomainEventCatalog.BookingRequested,
        DomainEventCatalog.CapacityExceeded
    };
    private static readonly IEnumerable<IEnumerable<string>> RequiredEventsByGroup = new[]
    {
        SuccessfulBookingEvents,
        FailedBookingEvents
    };
    private readonly TimeSpan _timeout;

    public ValidationRule(IOptions<ControlTowerConfiguration> options) => 
        _timeout = TimeSpan.FromMinutes(options.Value.TimeoutMinutes);

    internal bool AreValid(IReadOnlyCollection<TimestampedDomainEvent> domainEvents, DateTime executionDate)
    {
        if (AreAllRequiredEventsPresent(domainEvents))
        {
            return true;
        }

        return AreEventBeforeDeadline(domainEvents, executionDate);
    }

    private static bool AreAllRequiredEventsPresent(IEnumerable<DomainEvent> domainEvents) => 
        RequiredEventsByGroup.Any(requiredEvent => AreEqual(requiredEvent, domainEvents.Select(t => t.Name)));

    private static bool AreEqual(IEnumerable<string> events1, IEnumerable<string> events2) =>
        events1.OrderBy(_=>_).SequenceEqual(events2.OrderBy(_ => _));

    private bool AreEventBeforeDeadline(IEnumerable<TimestampedDomainEvent> events, DateTime executionDate)
    {
        var initialEventTime = events.MinBy(e=>e.Date)!.Date;
        var deadline = initialEventTime.Add(_timeout);

        return executionDate < deadline;
    }
}