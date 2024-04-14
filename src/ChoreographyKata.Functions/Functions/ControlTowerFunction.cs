using Azure.Messaging;
using ChoreographyKata.ControlTower;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace ChoreographyKata.Functions.Functions;

public sealed class ControlTowerFunction
{
    private readonly ILogger<ControlTowerFunction> _logger;
    private readonly ControlTowerService _controlTowerService;

    public ControlTowerFunction(ILogger<ControlTowerFunction> logger, ControlTowerService controlTowerService)
    {
        _logger = logger;
        _controlTowerService = controlTowerService;
    }

    [Function(nameof(Capture))]
    public void Capture([EventGridTrigger] CloudEvent cloudEvent)
    {
        _logger.LogInformation("{functionName} called on event type {eventType}, subject {eventSubject}.",
            nameof(Capture),
            cloudEvent.Type,
            cloudEvent.Subject);

        if (cloudEvent.Data == null)
        {
            return;
        }

        _controlTowerService.OnMessage(cloudEvent.Data.ToObjectFromJson<TheaterEvent>());
    }

    [Function(nameof(Inspect))]
    public void Inspect([TimerTrigger("0 2 * * * *")] TimerInfo myTimer) =>
        _controlTowerService.InspectErrors();
}