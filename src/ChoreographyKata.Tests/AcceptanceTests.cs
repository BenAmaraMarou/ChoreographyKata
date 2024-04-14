using FluentAssertions;

namespace ChoreographyKata.Tests;

public class AcceptanceTests
{
    [Test]
    public void SuccessfulBooking()
    {
        var inventory = new InventoryService(10);
        var notification = new NotificationService();
        var messageBus = new MessageBus();
        var booking = new BookingService(new Orchestration(inventory, new TicketingService(), notification), messageBus);

        booking.Book(3);

        inventory.AvailableSeats().Should().Be(7);
    }

    [Test]
    public void FailingBooking()
    {
        var inventory = new InventoryService(10);
        var notification = new NotificationService();
        var messageBus = new MessageBus();
        var booking = new BookingService(new Orchestration(inventory, new TicketingService(), notification), messageBus);

        booking.Book(11);

        inventory.AvailableSeats().Should().Be(10);
    }
}