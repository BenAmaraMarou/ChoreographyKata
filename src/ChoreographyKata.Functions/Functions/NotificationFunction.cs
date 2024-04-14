using Azure.Messaging;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace ChoreographyKata.Functions.Functions;

public sealed class NotificationFunction
{
    private readonly ILogger<NotificationFunction> _logger;
    private readonly NotificationService _notificationService;

    public NotificationFunction(ILogger<NotificationFunction> logger, NotificationService notificationService)
    {
        _logger = logger;
        _notificationService = notificationService;
    }

    [Function(nameof(Notify))]
    public void Notify([EventGridTrigger] CloudEvent cloudEvent)
    {
        _logger.LogInformation("{functionName} called on event type {eventType}, subject {eventSubject}.",
            nameof(Notify),
            cloudEvent.Type,
            cloudEvent.Subject);

        if (cloudEvent.Data == null)
        {
            return;
        }

        _notificationService.OnMessage(cloudEvent.Data.ToObjectFromJson<TheaterEvent>());
    }
}