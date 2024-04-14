namespace ChoreographyKata;

public sealed class BookingService
{
    private readonly MessageBus _messageBus;

    public BookingService(MessageBus messageBus)
    {
        _messageBus = messageBus;
    }

    public void Book(int numberOfSeats)
    {
        // validation logic goes here...
        var bookingReserved = new TheaterEvent(TheaterEvents.BookingReserved, numberOfSeats);
        _messageBus.Send(bookingReserved);
        Console.WriteLine($"{bookingReserved.Name} {bookingReserved.Value}");
    }
}