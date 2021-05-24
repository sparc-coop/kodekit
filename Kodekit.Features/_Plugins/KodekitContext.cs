using Microsoft.EntityFrameworkCore;
using Kodekit.Core;

namespace Kodekit.Features
{
    public partial class KodekitContext : DbContext
    {
        public KodekitContext(DbContextOptions<KodekitContext> options) : 
            base(options) { }

        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Kit> Kit { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(builder => {
                builder.HasKey(x => x.Id);
            });

            modelBuilder.Entity<Kit>(builder => {
                builder.HasKey(x => x.Id);
            });
        }

    }
}
