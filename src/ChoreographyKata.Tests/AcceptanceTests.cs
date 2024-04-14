using FluentAssertions;

namespace ChoreographyKata.Tests;

public class AcceptanceTests
{
    [Test]
    public void SuccessfulBooking()
    {
        var messageBus = new MessageBus();
        var booking = new BookingService(messageBus);
        var inventory = new InventoryService(10, messageBus);
        var ticketing = new TicketingService();
        var notification = new NotificationService();
        messageBus.Subscribe(inventory);
        messageBus.Subscribe(ticketing);
        messageBus.Subscribe(notification);

        booking.Book(3);

        inventory.AvailableSeats().Should().Be(7);
    }

    [Test]
    public void FailingBooking()
    {
        var messageBus = new MessageBus();
        var booking = new BookingService(messageBus);
        var inventory = new InventoryService(10, messageBus);
        var ticketing = new TicketingService();
        var notification = new NotificationService();
        messageBus.Subscribe(inventory);
        messageBus.Subscribe(ticketing);
        messageBus.Subscribe(notification);

        booking.Book(11);

        inventory.AvailableSeats().Should().Be(10);
    }
}