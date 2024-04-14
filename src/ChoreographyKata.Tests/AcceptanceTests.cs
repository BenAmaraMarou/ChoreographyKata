using ChoreographyKata.Broker;
using ChoreographyKata.Calendar;
using ChoreographyKata.CorrelationId;
using ChoreographyKata.Logging;
using FluentAssertions;
using NSubstitute;
using System.Globalization;
using ChoreographyKata.InspectedTheaterEvents;

namespace ChoreographyKata.Tests;

public class AcceptanceTests
{
    private static readonly Guid CorrelationId = Guid.Parse("80c38889-596c-4915-a5c6-4362962ed91e");
    private readonly ILogging _logging = Substitute.For<ILogging>();
    private readonly ICorrelationIdFactory _correlationIdFactory = Substitute.For<ICorrelationIdFactory>();
    private readonly ICalendar _calendar = Substitute.For<ICalendar>();

    public AcceptanceTests()
    {
        _correlationIdFactory.New().Returns(CorrelationId);
    }

    [Test]
    public void SuccessfulBooking()
    {
        var messageBus = new InMemoryMessageBus();
        var booking = new BookingService(messageBus, _logging, _correlationIdFactory);
        var inventory = new InventoryService(messageBus, _logging, 10);
        var ticketing = new TicketingService(_logging);
        var notification = new NotificationService(_logging);
        messageBus.Subscribe(inventory);
        messageBus.Subscribe(ticketing);
        messageBus.Subscribe(notification);

        booking.Book(3);

        inventory.AvailableSeats().Should().Be(7);
    }

    [Test]
    public void FailingBooking()
    {
        var messageBus = new InMemoryMessageBus();
        var booking = new BookingService(messageBus, _logging, _correlationIdFactory);
        var inventory = new InventoryService(messageBus, _logging, 10);
        var ticketing = new TicketingService(_logging);
        var notification = new NotificationService(_logging);
        messageBus.Subscribe(inventory);
        messageBus.Subscribe(ticketing);
        messageBus.Subscribe(notification);

        booking.Book(11);

        inventory.AvailableSeats().Should().Be(10);
    }
    
    [Test]
    public void SuccessfulControlTowerInspection()
    {
        var messageBus = new InMemoryMessageBus();
        var booking = new BookingService(messageBus, _logging, _correlationIdFactory);
        var inventory = new InventoryService(messageBus, _logging, 10);
        var ticketing = new TicketingService(_logging);
        var notification = new NotificationService(_logging);
        var controlTower = new ControlTowerService(new InMemoryTheaterEvents(), _calendar, _logging);
        messageBus.Subscribe(inventory);
        messageBus.Subscribe(ticketing);
        messageBus.Subscribe(notification);
        messageBus.Subscribe(controlTower);
        _calendar.Now().Returns(DateTime.Parse("2024-01-01 00:00:00", new CultureInfo("fr-FR"), DateTimeStyles.None));

        booking.Book(3);

        controlTower.CapturedEvents().Should().BeEquivalentTo(new[]
        {
            new TheaterEvent(CorrelationId, TheaterEvents.BookingReserved, 3),
            new TheaterEvent(CorrelationId, TheaterEvents.CapacityReserved, 3)
        });
    }
}
