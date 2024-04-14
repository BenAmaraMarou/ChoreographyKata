using Azure;
using Azure.Messaging.EventGrid;
using ChoreographyKata.Broker.Configuration;
using Microsoft.Extensions.Options;

namespace ChoreographyKata.Broker;

public sealed class EventGrid : IMessageBus
{
    private readonly EventGridConfiguration _configuration;

    public EventGrid(IOptions<EventGridConfiguration> options)
    {
        _configuration = options.Value;
    }

    //Subscription is made at cloud resource

    public async Task Publish(DomainEvent domainEvent)
    {
        var client = new EventGridPublisherClient(
            new Uri(_configuration.Endpoint),
            new AzureKeyCredential(_configuration.AccessKey));

        var egEvent = new EventGridEvent(
                domainEvent.Name,
                nameof(DomainEvent),
                _configuration.Version,
                domainEvent);

        await client.SendEventAsync(egEvent);
    }
}