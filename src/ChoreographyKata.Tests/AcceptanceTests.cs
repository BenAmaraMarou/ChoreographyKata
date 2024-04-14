using ChoreographyKata.Broker;
using ChoreographyKata.Calendar;
using ChoreographyKata.CorrelationId;
using ChoreographyKata.Logging;
using FluentAssertions;
using NSubstitute;
using System.Globalization;
using ChoreographyKata.ControlTower;
using ChoreographyKata.ControlTower.Configuration;
using ChoreographyKata.ControlTower.EventLog;
using Microsoft.Extensions.Options;

namespace ChoreographyKata.Tests;

public sealed class AcceptanceTests
{
    private static readonly Guid CorrelationId1 = Guid.Parse("80c38889-596c-4915-a5c6-4362962ed91e");
    private static readonly Guid CorrelationId2 = Guid.Parse("5478d4d9-dfeb-4970-a752-928d831077cf");
    private static readonly DateTime Date1 = DateTime.Parse("2024-01-01 00:00:00", new CultureInfo("fr-FR"), DateTimeStyles.None);
    private static readonly DateTime Date2 = DateTime.Parse("2024-01-01 01:00:00", new CultureInfo("fr-FR"), DateTimeStyles.None);
    private static readonly IOptions<ControlTowerConfiguration> ControlTowerConfig = new ControlTowerConfiguration{TimeoutMinutes = 10}.CreateOptions();
    private readonly ILogging _logging = Substitute.For<ILogging>();
    private readonly ICorrelationIdFactory _correlationIdFactory = Substitute.For<ICorrelationIdFactory>();
    private readonly ICalendar _calendar = Substitute.For<ICalendar>();

    [SetUp]
    public void SetUp()
    {
        _correlationIdFactory.New().Returns(CorrelationId1, CorrelationId2);
    }

    [Test]
    public async Task SuccessfulBooking()
    {
        var messageBus = new InMemoryMessageBus();
        var booking = new BookingService(messageBus, _logging, _correlationIdFactory);
        var inventory = new InventoryService(messageBus, _logging, Capacity(10));
        var ticketing = new TicketingService(_logging);
        var notification = new NotificationService(_logging);
        messageBus.Subscribe(inventory);
        messageBus.Subscribe(ticketing);
        messageBus.Subscribe(notification);

        await booking.RequestBookingAsync(3);

        inventory.AvailableSeats().Should().Be(7);
    }

    [Test]
    public async Task FailingBooking()
    {
        var messageBus = new InMemoryMessageBus();
        var booking = new BookingService(messageBus, _logging, _correlationIdFactory);
        var inventory = new InventoryService(messageBus, _logging, Capacity(10));
        var ticketing = new TicketingService(_logging);
        var notification = new NotificationService(_logging);
        messageBus.Subscribe(inventory);
        messageBus.Subscribe(ticketing);
        messageBus.Subscribe(notification);

        await booking.RequestBookingAsync(11);

        inventory.AvailableSeats().Should().Be(10);
    }
    
    [Test]
    public async Task SuccessfulInspection()
    {
        var messageBus = new InMemoryMessageBus();
        var booking = new BookingService(messageBus, _logging, _correlationIdFactory);
        var inventory = new InventoryService(messageBus, _logging, Capacity(10));
        var ticketing = new TicketingService(_logging);
        var notification = new NotificationService(_logging);
        var controlTower = new ControlTowerService(new InMemoryEventLog(), _calendar, _logging, new ValidationRule(ControlTowerConfig));
        messageBus.Subscribe(inventory);
        messageBus.Subscribe(ticketing);
        messageBus.Subscribe(notification);
        messageBus.Subscribe(controlTower);
        _calendar.Now().Returns(Date1);

        await booking.RequestBookingAsync(3);
        await booking.RequestBookingAsync(11);

        (await controlTower.CapturedEventsAsync()).Should().BeEquivalentTo(new[]
        {
            new DomainEvent(CorrelationId1, DomainEventCatalog.BookingRequested, 3),
            new DomainEvent(CorrelationId1, DomainEventCatalog.InventoryReserved, 3),
            new DomainEvent(CorrelationId2, DomainEventCatalog.BookingRequested, 11),
            new DomainEvent(CorrelationId2, DomainEventCatalog.CapacityExceeded, 11)
        });
        (await controlTower.GetKoCorrelationIdsAsync()).Should().BeEmpty();
    }
    
    [Test]
    public async Task FailedInspection()
    {
        var messageBus = new InMemoryMessageBus();
        var booking = new BookingService(messageBus, _logging, _correlationIdFactory);
        var inventory = Substitute.For<IListener>();
        var ticketing = new TicketingService(_logging);
        var notification = new NotificationService(_logging);
        var controlTower = new ControlTowerService(new InMemoryEventLog(), _calendar, _logging, new ValidationRule(ControlTowerConfig));
        messageBus.Subscribe(inventory);
        messageBus.Subscribe(ticketing);
        messageBus.Subscribe(notification);
        messageBus.Subscribe(controlTower);
        _calendar.Now().Returns(Date1);

        await booking.RequestBookingAsync(3);

        _calendar.Now().Returns(Date2);
        (await controlTower.CapturedEventsAsync()).Should().BeEquivalentTo(new[]
        {
            new DomainEvent(CorrelationId1, DomainEventCatalog.BookingRequested, 3),
        });
        (await controlTower.GetKoCorrelationIdsAsync()).Should().BeEquivalentTo(new[]
        {
            CorrelationId1
        });
    }

    private static IOptions<InventoryConfiguration> Capacity(int capacity) =>
        new InventoryConfiguration { Capacity = capacity }.CreateOptions();
}