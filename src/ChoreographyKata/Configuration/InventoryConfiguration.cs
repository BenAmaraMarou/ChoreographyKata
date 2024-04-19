namespace ChoreographyKata.Configuration;

public sealed record InventoryConfiguration
{
    public const string SectionKey = "Inventory";

    public int Capacity { get; init; }
}