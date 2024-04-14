namespace ChoreographyKata.ControlTower.Configuration;

public sealed record ControlTowerConfiguration
{
    public const string SectionKey = "ControlTower";

    public uint TimeoutMinutes { get; init; }
}