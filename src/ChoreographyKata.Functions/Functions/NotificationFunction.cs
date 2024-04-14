using Azure.Messaging;
using Microsoft.Azure.Functions.Worker;
using ChoreographyKata.Logging;


namespace ChoreographyKata.Functions.Functions;

public sealed class NotificationFunction
{
    private readonly ILogging _logger;
    private readonly NotificationService _notificationService;

    public NotificationFunction(ILogging logger, NotificationService notificationService)
    {
        _logger = logger;
        _notificationService = notificationService;
    }

    [Function(nameof(Notify))]
    public async Task Notify([EventGridTrigger] CloudEvent cloudEvent)
    {
        _logger.Log($"{nameof(Notify)} called on event type {cloudEvent.Type}, subject {cloudEvent.Subject}.");

        if (cloudEvent.Data == null)
        {
            return;
        }

        await _notificationService.OnMessageAsync(cloudEvent.Data.ToObjectFromJson<TheaterEvent>());
    }
}