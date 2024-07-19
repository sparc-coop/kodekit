using Microsoft.EntityFrameworkCore;
using Sparc.Blossom.Authentication;

namespace Kodekit;

public partial class KodekitContext
    (DbContextOptions<KodekitContext> options, BlossomClaimsPrincipalProvider auth) 
    : BlossomDbContext(options, auth)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<User>().HasNoKey();

        var kit = builder.Entity<Kit>().HasPartitionKey(x => x.KitId).HasQueryFilter(x => x.UserId == UserId);
        var revision = builder.Entity<KitRevision>().HasPartitionKey(x => x.KitId);
    }
}
