using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker;

namespace ChoreographyKata.Functions.Functions;

public sealed class BookingFunction
{
    private readonly BookingService _bookingService;

    public BookingFunction(BookingService bookingService)
    {
        _bookingService = bookingService;
    }

    [Function(nameof(RequestBooking))]
    public async Task RequestBooking(
        [HttpTrigger(AuthorizationLevel.Function, "post", Route = "request-booking/{numberOfSeats}")] HttpRequest req,
        int numberOfSeats)
    {
        await _bookingService.RequestBookingAsync(numberOfSeats);
    }
}