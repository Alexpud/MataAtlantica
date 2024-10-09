using Microsoft.EntityFrameworkCore;

namespace MataAtlantica.API.Infrastructure.Data;

public class MataAtlanticaDbContext : DbContext
{
    public MataAtlanticaDbContext(DbContextOptions<MataAtlanticaDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MataAtlanticaDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
