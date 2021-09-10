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
            headings.OwnsOne(x => x.FontFamily);
            headings.OwnsOne(x => x.FontWeight);
            headings.OwnsOne(x => x.FontSize);
            headings.OwnsOne(x => x.TypeScale);
            headings.OwnsOne(x => x.LineHeight);


            var paragraphs = kit.OwnsOne(x => x.Paragraphs);
            paragraphs.OwnsOne(x => x.FontFamily);
            paragraphs.OwnsOne(x => x.FontWeight);
            paragraphs.OwnsOne(x => x.FontSize);
            paragraphs.OwnsOne(x => x.TypeScale);
            paragraphs.OwnsOne(x => x.LineHeight);

            var buttons = kit.OwnsOne(x => x.Buttons);
            buttons.OwnsOne(x => x.FontWeight);
            buttons.OwnsOne(x => x.FontSize);
            buttons.OwnsOne(x => x.VerticalPadding);
            buttons.OwnsOne(x => x.HorizontalPadding);
            buttons.OwnsOne(x => x.CornerRadius);
            buttons.OwnsOne(x => x.BorderWidth);
            buttons.OwnsOne(x => x.IconHeight);
            buttons.OwnsOne(x => x.IconWidth);

            var inputs = kit.OwnsOne(x => x.Inputs);
            inputs.OwnsOne(x => x.FontWeight);
            inputs.OwnsOne(x => x.FontSize);
            inputs.OwnsOne(x => x.VerticalPadding);
            inputs.OwnsOne(x => x.HorizontalPadding);
            inputs.OwnsOne(x => x.CornerRadius);
            inputs.OwnsOne(x => x.BorderWidth);

            var selectors = kit.OwnsOne(x => x.Selectors);
            selectors.OwnsOne(x => x.FontWeight);
            selectors.OwnsOne(x => x.FontSize);
            selectors.OwnsOne(x => x.ActiveColor);

            var settings = kit.OwnsOne(x => x.Settings);

            var dropdowns = kit.OwnsOne(x => x.Dropdowns);
            dropdowns.OwnsOne(x => x.FontWeight);
            dropdowns.OwnsOne(x => x.FontSize);
            dropdowns.OwnsOne(x => x.VerticalPadding);
            dropdowns.OwnsOne(x => x.HorizontalPadding);
            dropdowns.OwnsOne(x => x.CornerRadius);
            dropdowns.OwnsOne(x => x.BorderWidth);
        }
    }
}
