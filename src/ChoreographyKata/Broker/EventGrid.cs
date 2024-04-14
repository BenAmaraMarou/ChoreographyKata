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

    public async Task SendAsync(TheaterEvent theaterEvent)
    {
        var client = new EventGridPublisherClient(
            new Uri(_configuration.Endpoint),
            new AzureKeyCredential(_configuration.AccessKey));

        var egEvent = new EventGridEvent(
                theaterEvent.Name,
                nameof(TheaterEvent),
                _configuration.Version,
                theaterEvent);

        await client.SendEventAsync(egEvent);
    }
}