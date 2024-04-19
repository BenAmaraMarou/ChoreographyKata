using ChoreographyKata.Logging;
using Microsoft.Extensions.Logging;

namespace ChoreographyKata.Functions.Registration;

public sealed class ConsoleLogger : ILogging
{
    private readonly ILogger<ConsoleLogger> _logger;

    public ConsoleLogger(ILogger<ConsoleLogger> logger)
    {
        _logger = logger;
    }

    public void Log<TDomainEvent>(TDomainEvent domainEvent) 
        where TDomainEvent : DomainEvent
        => _logger.LogInformation(domainEvent.ToString());

    public void Log(string message) => 
        _logger.LogInformation(message);
}