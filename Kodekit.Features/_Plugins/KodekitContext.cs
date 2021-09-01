using Microsoft.EntityFrameworkCore;

namespace Kodekit.Features
{
    public partial class KodekitContext : DbContext
    {
        public KodekitContext(DbContextOptions<KodekitContext> options) : 
            base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>().HasNoKey();
            builder.Entity<Kit>().HasPartitionKey(x => x.KitId);
        }
    }
}
