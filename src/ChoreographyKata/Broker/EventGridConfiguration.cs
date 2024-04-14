namespace ChoreographyKata.Broker;

public sealed record EventGridConfiguration
{
    public const string SectionKey = "EventGrid";

    public string TopicName { get; init; } = string.Empty;

    public string TopicEndpoint { get; init; } = string.Empty;

    public string Version { get; set; } = string.Empty;
}