namespace ChoreographyKata.Database;

public sealed record DomainEventEntity
{
    public int Id { get; set; }

    public Guid CorrelationId { get; set; }

    public string Name { get; set; } = string.Empty;

    public int Value { get; set; }

    public DateTime Date { get; set; }
}