using Microsoft.EntityFrameworkCore;

namespace Kodekit;

public partial class KodekitContext(DbContextOptions<KodekitContext> options, IHttpContextAccessor auth) 
    : BlossomDbContext(options, auth)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<User>().ToContainer("Users").HasPartitionKey(x => x.UserId);

        var kit = builder.Entity<Kit>().HasPartitionKey(x => x.KitId).HasQueryFilter(x => x.UserId == UserId);
        var revision = builder.Entity<KitRevision>().HasPartitionKey(x => x.KitId);
    }
}
