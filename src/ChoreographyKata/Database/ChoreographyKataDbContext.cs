using Microsoft.EntityFrameworkCore;

namespace ChoreographyKata.Database;

public sealed class ChoreographyKataDbContext : DbContext
{
    public ChoreographyKataDbContext(DbContextOptions<ChoreographyKataDbContext> options) : base(options)
    {
    }

    public DbSet<TheaterEventEntity> TheaterEvents => Set<TheaterEventEntity>();
}