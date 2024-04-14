using Azure.Messaging;
using ChoreographyKata.ControlTower;
using ChoreographyKata.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker;

namespace ChoreographyKata.Functions.Functions;

public sealed class ControlTowerFunction
{
    private readonly ILogging _logger;
    private readonly ControlTowerService _controlTowerService;

    public ControlTowerFunction(ILogging logger, ControlTowerService controlTowerService)
    {
        _logger = logger;
        _controlTowerService = controlTowerService;
    }

    [Function(nameof(Capture))]
    public async Task Capture([EventGridTrigger] CloudEvent cloudEvent)
    {
        _logger.Log($"{nameof(Capture)} called on event type {cloudEvent.Type}, subject {cloudEvent.Subject}.");
        if (cloudEvent.Data == null)
        {
            return;
        }

        await _controlTowerService.OnMessageAsync(cloudEvent.Data.ToObjectFromJson<TheaterEvent>());
    }

    [Function(nameof(Inspect))]
    public async Task Inspect([TimerTrigger("0 2 * * * *")] TimerInfo myTimer) =>
        await _controlTowerService.InspectErrorsAsync();
}