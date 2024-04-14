namespace ChoreographyKata;

public sealed class BookingService
{
    private readonly Orchestration _orchestration;

    public BookingService(Orchestration orchestration)
    {
        _orchestration = orchestration;
    }

    public void Book(int numberOfSeats)
    {
        // validation logic goes here...
        Console.WriteLine($"{numberOfSeats} booking requested");
        _orchestration.Launch(numberOfSeats);
    }
}