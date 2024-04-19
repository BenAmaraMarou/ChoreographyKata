using Azure.Messaging;
using ChoreographyKata.EventLogs;
using Microsoft.Azure.Functions.Worker;

namespace ChoreographyKata.Functions.Functions;

public sealed class CaptureEventsFunction
{
    private readonly CaptureEventsService _captureEventsService;

    public CaptureEventsFunction(CaptureEventsService captureEventsService)
    {
        _captureEventsService = captureEventsService;
    }


    [Function(nameof(CaptureEvent))]
    public async Task CaptureEvent([EventGridTrigger] CloudEvent cloudEvent)
    {
        var domainEvent = cloudEvent.Data!.ToObjectFromJson<DomainEvent>();

        await _captureEventsService.OnMessageAsync(domainEvent);
    }
}