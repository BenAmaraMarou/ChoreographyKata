using Azure.Messaging;
using Microsoft.Azure.Functions.Worker;

namespace ChoreographyKata.Functions.Functions;

public sealed class NotificationFunction
{
    private readonly NotificationService _notificationService;

    public NotificationFunction(NotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    [Function(nameof(Notify))]
    public async Task Notify([EventGridTrigger] CloudEvent cloudEvent)
    {
        var domainEvent = cloudEvent.Data!.ToObjectFromJson<DomainEvent>();

        await _notificationService.OnMessageAsync(domainEvent);
    }
}