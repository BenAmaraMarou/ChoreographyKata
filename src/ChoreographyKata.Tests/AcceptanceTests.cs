using FluentAssertions;

namespace ChoreographyKata.Tests;

public class AcceptanceTests
{
    [Test]
    public void SuccessfulBooking()
    {
        var messageBus = new MessageBus();
        var inventory = new InventoryService(10, messageBus);
        var notification = new NotificationService();
        var booking = new BookingService(new Orchestration(inventory, new TicketingService(), notification), messageBus);

        booking.Book(3);

        inventory.AvailableSeats().Should().Be(7);
    }

    [Test]
    public void FailingBooking()
    {
        var messageBus = new MessageBus();
        var inventory = new InventoryService(10, messageBus);
        var notification = new NotificationService();
        var booking = new BookingService(new Orchestration(inventory, new TicketingService(), notification), messageBus);

        booking.Book(11);

        inventory.AvailableSeats().Should().Be(10);
    }
}