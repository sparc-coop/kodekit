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
            
            var kit = builder.Entity<Kit>().HasPartitionKey(x => x.Id);

            var colors = kit.OwnsMany(x => x.Colors);
            colors.OwnsOne(x => x.Value);

            var shadows = kit.OwnsMany(x => x.Shadows);
            shadows.OwnsOne(x => x.Value);

            var headings = kit.OwnsOne(x => x.Headings);
            headings.OwnsOne(x => x.Font);

            var paragraphs = kit.OwnsOne(x => x.Paragraphs);
            paragraphs.OwnsOne(x => x.Font);

            var buttons = kit.OwnsOne(x => x.Buttons);
            buttons.OwnsOne(x => x.Font);
            buttons.OwnsOne(x => x.Border);
            buttons.OwnsOne(x => x.Padding);
            buttons.OwnsOne(x => x.IconHeight);
            buttons.OwnsOne(x => x.IconWidth);

            var inputs = kit.OwnsOne(x => x.Inputs);
            inputs.OwnsOne(x => x.Font);
            inputs.OwnsOne(x => x.Border);
            inputs.OwnsOne(x => x.Padding);

            var selectors = kit.OwnsOne(x => x.Selectors);
            selectors.OwnsOne(x => x.Font);
            selectors.OwnsOne(x => x.ActiveColor);

            var settings = kit.OwnsOne(x => x.Settings);

            var dropdowns = kit.OwnsOne(x => x.Dropdowns);
            dropdowns.OwnsOne(x => x.Font);
            dropdowns.OwnsOne(x => x.Border);
            dropdowns.OwnsOne(x => x.Padding);
        }
    }
}
