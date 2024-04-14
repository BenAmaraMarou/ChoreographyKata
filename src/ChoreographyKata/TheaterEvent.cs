namespace ChoreographyKata;

public sealed record TheaterEvent(Guid CorrelationId, string Name, int Value);