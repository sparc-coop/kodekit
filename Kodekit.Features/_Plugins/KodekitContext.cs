using Microsoft.EntityFrameworkCore;
using Kodekit.Core;

namespace Kodekit.Features
{
    public partial class KodekitContext : DbContext
    {
        public KodekitContext(DbContextOptions<KodekitContext> options) : 
            base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            var user = builder.Entity<User>().HasPartitionKey(x => x.UserId);
            //user.OwnsMany(x => x.Kits);

            builder.Entity<Kit>().HasPartitionKey(x => x.UserId);
        }

    }
}
