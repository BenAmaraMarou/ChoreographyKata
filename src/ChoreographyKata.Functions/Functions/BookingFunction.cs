using ChoreographyKata.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker;

namespace ChoreographyKata.Functions.Functions;

public sealed class BookingFunction
{
    private readonly ILogging _logger;
    private readonly BookingService _bookingService;

    public BookingFunction(ILogging logger, BookingService bookingService)
    {
        _logger = logger;
        _bookingService = bookingService;
    }

    [Function(nameof(BookSeats))]
    public async Task BookSeats(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "book/{numberOfSeats}")] HttpRequest req,
        int numberOfSeats)
    {
        _logger.Log($"{nameof(BookSeats)} {numberOfSeats} called.");
        await _bookingService.BookAsync(numberOfSeats);
    }
}