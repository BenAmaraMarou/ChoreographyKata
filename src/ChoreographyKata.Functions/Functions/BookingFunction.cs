using ChoreographyKata.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace ChoreographyKata.Functions.Functions;

public sealed class BookingFunction
{
    private readonly BookingService _bookingService;

    public BookingFunction(BookingService bookingService)
    {
        _bookingService = bookingService;
    }

    [Function(nameof(BookSeats))]
    public async Task BookSeats(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "book/{numberOfSeats}")] HttpRequest req,
        int numberOfSeats)
    {
        await _bookingService.BookAsync(numberOfSeats);
    }
}