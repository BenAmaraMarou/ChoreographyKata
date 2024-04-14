using ChoreographyKata.Database;
using Microsoft.EntityFrameworkCore;

namespace ChoreographyKata.ControlTower.EventLog;

public sealed class DbEventLog : IEventLog
{
    private readonly IDbContextFactory<ChoreographyKataDbContext> _dbContextFactory;

    public DbEventLog(IDbContextFactory<ChoreographyKataDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task AppendAsync(TimestampedDomainEvent domainEvent)
    {
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync();
        await dbContext.DomainEvents.AddAsync(new DomainEventEntity
        {
            CorrelationId = domainEvent.CorrelationId,
            Name = domainEvent.Name,
            Value = domainEvent.Value,
            Date = domainEvent.Date,
        });
       await dbContext.SaveChangesAsync();
    }

    public async Task<IReadOnlyCollection<TimestampedDomainEvent>> GetAsync()
    {
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync();

        return (await dbContext.DomainEvents.ToListAsync()).Select(CreateEvent).ToList();
    }

    private static TimestampedDomainEvent CreateEvent(DomainEventEntity e) 
        => new(e.CorrelationId, e.Name, e.Value, e.Date);
}