using ChoreographyKata.Broker;
using ChoreographyKata.CorrelationId;

namespace ChoreographyKata;

public sealed class BookingService
{
    private readonly IMessageBus _messageBus;
    private readonly ILogger _logger;
    private readonly ICorrelationIdFactory _correlationIdFactory;

    public BookingService(IMessageBus messageBus, ILogger logger, ICorrelationIdFactory correlationIdFactory)
    {
        _messageBus = messageBus;
        _logger = logger;
        _correlationIdFactory = correlationIdFactory;
    }

    public void Book(int numberOfSeats)
    {
        // validation logic goes here...
        if (numberOfSeats <= 0)
        {
            return;
        }

        var bookingReserved =
            new TheaterEvent(_correlationIdFactory.New(), 
                TheaterEvents.BookingReserved, 
                numberOfSeats);
        _messageBus.SendAsync(bookingReserved);
        _logger.Log(bookingReserved);
    }
}