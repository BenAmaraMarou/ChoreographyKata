using ChoreographyKata.Broker;

namespace ChoreographyKata;

public sealed class BookingService
{
    private readonly IMessageBus _messageBus;
    private readonly ILogger _logger;

    public BookingService(IMessageBus messageBus, ILogger logger)
    {
        _messageBus = messageBus;
        _logger = logger;
    }

    public void Book(int numberOfSeats)
    {
        // validation logic goes here...
        var bookingReserved = new TheaterEvent(TheaterEvents.BookingReserved, numberOfSeats);
        _messageBus.SendAsync(bookingReserved);
        _logger.Log(bookingReserved);
    }
}