using ChoreographyKata.Logging;
using Microsoft.Extensions.Logging;

namespace ChoreographyKata.Functions.Registration;

public sealed class TheaterLogger : ILogging
{
    private readonly ILogger<TheaterLogger> _logger;

    public TheaterLogger(ILogger<TheaterLogger> logger)
    {
        _logger = logger;
    }

    public void Log(TheaterEvent theaterEvent) => 
        _logger.LogInformation(theaterEvent.ToString());

    public void Log(string message) => 
        _logger.LogInformation(message);
}