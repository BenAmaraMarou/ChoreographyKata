using Azure.Messaging;
using ChoreographyKata.ControlTower;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker;

namespace ChoreographyKata.Functions.Functions;

public sealed class ControlTowerFunction
{
    private readonly ControlTowerService _controlTowerService;

    public ControlTowerFunction(ControlTowerService controlTowerService)
    {
        _controlTowerService = controlTowerService;
    }

    [Function(nameof(Capture))]
    public async Task Capture([EventGridTrigger] CloudEvent cloudEvent)
    {
        await _controlTowerService.OnMessageAsync(cloudEvent.Data!.ToObjectFromJson<TheaterEvent>());
    }

    [Function(nameof(Inspect))]
    public async Task Inspect([TimerTrigger("0 */2 * * * *")] TimerInfo myTimer) =>
        await _controlTowerService.InspectErrorsAsync();
}