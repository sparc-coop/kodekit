using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Kodekit;

public partial class KodekitContext(DbContextOptions<KodekitContext> options, IPublisher publisher, IHttpContextAccessor auth) 
    : BlossomDbContext(options, publisher, auth)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<User>().ToContainer("Users").HasPartitionKey(x => x.UserId);

        var kit = builder.Entity<Kit>().HasPartitionKey(x => x.KitId).HasQueryFilter(x => x.UserId == UserId);
        var revision = builder.Entity<KitRevision>().HasPartitionKey(x => x.KitId);
    }
}
