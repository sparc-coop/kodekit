using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace UI_Kit.Model
{
    public partial class UiKitContext : DbContext
    {
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Kit> Kit { get; set; }

        public UiKitContext()
        { }

        public UiKitContext(DbContextOptions<UiKitContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(ConfigureUser);
            modelBuilder.Entity<Kit>(ConfigureKit);
        }

        private void ConfigureUser(EntityTypeBuilder<User> entity)
        {

        }

        private void ConfigureKit(EntityTypeBuilder<Kit> entity)
        {

        }
    }
}
