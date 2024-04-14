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

    public async Task RequestBookingAsync(int numberOfSeats)
    {
        // validation logic goes here...
        if (numberOfSeats <= 0)
        {
            return;
        }

        var bookingReserved =
            new DomainEvent(_correlationIdFactory.New(), 
                DomainEventCatalog.BookingRequested, 
                numberOfSeats);

        await _messageBus.Publish(bookingReserved);

        _logging.Log(bookingReserved);
    }
}