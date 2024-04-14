using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace ChoreographyKata.Functions.Functions;

public sealed class BookingFunction
{
    private readonly ILogger<BookingFunction> _logger;
    private readonly BookingService _bookingService;

    public BookingFunction(ILogger<BookingFunction> logger, BookingService bookingService)
    {
        _logger = logger;
        _bookingService = bookingService;
    }

    [Function(nameof(BookSeats))]
    public void BookSeats(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "book/{numberOfSeats}")] HttpRequest req,
        int numberOfSeats)
    {
        _logger.LogInformation("{functionName} {numberOfSeats} called.", nameof(BookSeats), numberOfSeats);
        _bookingService.Book(numberOfSeats);
    }
}