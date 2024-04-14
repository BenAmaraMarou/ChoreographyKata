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
        _messageBus.Send(new TheaterEvent(TheaterEvents.BookingReserved, numberOfSeats));
        Console.WriteLine($"{numberOfSeats} {TheaterEvents.BookingReserved}");
    }
}