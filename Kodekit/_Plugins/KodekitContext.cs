using Microsoft.EntityFrameworkCore;

namespace Kodekit;

public partial class KodekitContext : DbContext
{
    public KodekitContext(DbContextOptions<KodekitContext> options) :
        base(options)
    { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<User>().HasNoKey();

        var kit = builder.Entity<Kit>().HasPartitionKey(x => x.KitId);
        var revision = builder.Entity<KitRevision>().HasPartitionKey(x => x.KitId);
    }
}
