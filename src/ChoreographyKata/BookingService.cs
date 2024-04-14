using ChoreographyKata.Broker;

namespace ChoreographyKata;

public sealed class BookingService
{
    private readonly IMessageBus _messageBus;

    public BookingService(IMessageBus messageBus)
    {
        _messageBus = messageBus;
    }

    public void Book(int numberOfSeats)
    {
        // validation logic goes here...
        var bookingReserved = new TheaterEvent(TheaterEvents.BookingReserved, numberOfSeats);
        _messageBus.SendAsync(bookingReserved);
        Console.WriteLine($"{bookingReserved.Name} {bookingReserved.Value}");
    }
}