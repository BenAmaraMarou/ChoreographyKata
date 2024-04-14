namespace ChoreographyKata.Broker.Configuration;

public sealed record EventGridConfiguration
{
    public const string SectionKey = "EventGrid";

    public string Endpoint { get; init; } = string.Empty;

    public string AccessKey { get; set; } = string.Empty;

    public string Version { get; set; } = string.Empty;
}