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
        _messageBus.Send(new TheaterEvent("BookingRequested", numberOfSeats));
        Console.WriteLine($"{numberOfSeats} booking requested");
        _orchestration.Launch(numberOfSeats);
    }
}