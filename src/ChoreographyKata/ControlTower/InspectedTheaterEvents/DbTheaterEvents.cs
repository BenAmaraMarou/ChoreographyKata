using ChoreographyKata.Database;
using Microsoft.EntityFrameworkCore;

namespace ChoreographyKata.ControlTower.InspectedTheaterEvents;

public sealed class DbTheaterEvents : ITheaterEvents
{
    private readonly IDbContextFactory<ChoreographyKataDbContext> _dbContextFactory;

    public DbTheaterEvents(IDbContextFactory<ChoreographyKataDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task AddAsync(TheaterEvent theaterEvent, DateTime dateTime)
    {
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync();
        await dbContext.TheaterEvents.AddAsync(new TheaterEventEntity
        {
            CorrelationId = theaterEvent.CorrelationId,
            Name = theaterEvent.Name,
            Value = theaterEvent.Value,
            Date = dateTime
        });
       await dbContext.SaveChangesAsync();
    }

    public async Task<IReadOnlyDictionary<TheaterEvent, DateTime>> GetAsync()
    {
        await using var dbContext = await _dbContextFactory.CreateDbContextAsync();

        return (await dbContext.TheaterEvents.ToListAsync())
            .ToDictionary(CreateTheaterEvent, e => e.Date);
    }

    private static TheaterEvent CreateTheaterEvent(TheaterEventEntity e) => new(e.CorrelationId, e.Name, e.Value);
}