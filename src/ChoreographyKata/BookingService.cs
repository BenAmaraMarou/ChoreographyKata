namespace ChoreographyKata;

public sealed class BookingService
{
    private readonly Orchestration _orchestration;
    private readonly MessageBus _messageBus;

    public BookingService(Orchestration orchestration, MessageBus messageBus)
    {
        _orchestration = orchestration;
        _messageBus = messageBus;
    }

    public void Book(int numberOfSeats)
    {
        // validation logic goes here...
        _messageBus.Send(new TheaterEvent(TheaterEvents.BookingReserved, numberOfSeats));
        Console.WriteLine($"{numberOfSeats} {TheaterEvents.BookingReserved}");
        _orchestration.Launch(numberOfSeats);
    }
}