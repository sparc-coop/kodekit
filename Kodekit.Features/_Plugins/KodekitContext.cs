using Microsoft.EntityFrameworkCore;
using Kodekit.Core;

namespace Kodekit.Features
{
    public partial class KodekitContext : DbContext
    {
        public KodekitContext(DbContextOptions<KodekitContext> options) : 
            base(options) { }

        //public virtual DbSet<User> User { get; set; }
        //public virtual DbSet<Kit> Kit { get; set; }

        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    builder.Entity<User>(builder => {
        //        builder.HasKey(x => x.Id);
        //    });

        //    builder.Entity<Kit>(builder => {
        //        builder.HasKey(x => x.Id);
        //    });
        //}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            var user = builder.Entity<User>().HasPartitionKey(x => x.UserId);
            //user.OwnsMany(x => x.Kits);

            builder.Entity<Kit>().HasPartitionKey(x => x.UserId);
        }

    }
}
