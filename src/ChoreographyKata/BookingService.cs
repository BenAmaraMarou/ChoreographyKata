using ChoreographyKata.Broker;
using ChoreographyKata.CorrelationId;
using ChoreographyKata.Logging;

namespace ChoreographyKata;

public sealed class BookingService
{
    private readonly IMessageBus _messageBus;
    private readonly ILogging _logging;
    private readonly ICorrelationIdFactory _correlationIdFactory;

    public BookingService(IMessageBus messageBus, ILogging logging, ICorrelationIdFactory correlationIdFactory)
    {
        _messageBus = messageBus;
        _logging = logging;
        _correlationIdFactory = correlationIdFactory;
    }

    public async Task BookAsync(int numberOfSeats)
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
        await _messageBus.SendAsync(bookingReserved);
        _logging.Log(bookingReserved);
    }
}