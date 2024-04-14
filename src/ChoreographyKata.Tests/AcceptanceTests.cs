using ChoreographyKata.Broker;
using ChoreographyKata.CorrelationId;
using FluentAssertions;
using NSubstitute;

namespace ChoreographyKata.Tests;

public class AcceptanceTests
{
    private readonly ILogger _logger = Substitute.For<ILogger>();
    private readonly ICorrelationIdFactory _correlationIdFactory = new CorrelationIdFactory();

    [Test]
    public void SuccessfulBooking()
    {
        var messageBus = new InMemoryMessageBus();
        var booking = new BookingService(messageBus, _logger, _correlationIdFactory);
        var inventory = new InventoryService(messageBus, _logger, 10);
        var ticketing = new TicketingService(_logger);
        var notification = new NotificationService(_logger);
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
        var booking = new BookingService(messageBus, _logger, _correlationIdFactory);
        var inventory = new InventoryService(messageBus, _logger, 10);
        var ticketing = new TicketingService(_logger);
        var notification = new NotificationService(_logger);
        messageBus.Subscribe(inventory);
        messageBus.Subscribe(ticketing);
        messageBus.Subscribe(notification);

        booking.Book(11);

        inventory.AvailableSeats().Should().Be(10);
    }
}