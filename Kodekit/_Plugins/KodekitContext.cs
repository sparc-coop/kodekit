using Microsoft.EntityFrameworkCore;

namespace Kodekit;

public partial class KodekitContext(DbContextOptions<KodekitContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<User>().HasNoKey();

        var kit = builder.Entity<Kit>().HasPartitionKey(x => x.KitId);
        var revision = builder.Entity<KitRevision>().HasPartitionKey(x => x.KitId);
    }
}
