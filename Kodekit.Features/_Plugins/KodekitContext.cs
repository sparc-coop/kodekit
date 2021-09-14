using Kodekit.Features.Elements;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
            headings.OwnsOne(x => x.Font).Configure();

            var paragraphs = kit.OwnsOne(x => x.Paragraphs);
            paragraphs.OwnsOne(x => x.Font).Configure();

            var buttons = kit.OwnsOne(x => x.Buttons);
            buttons.OwnsOne(x => x.Font).Configure();
            buttons.OwnsOne(x => x.Border).Configure();

            buttons.OwnsOne(x => x.Padding).Configure();
            buttons.OwnsOne(x => x.IconHeight);
            buttons.OwnsOne(x => x.IconWidth);

            var inputs = kit.OwnsOne(x => x.Inputs);
            inputs.OwnsOne(x => x.Font).Configure();
            inputs.OwnsOne(x => x.Border).Configure();
            inputs.OwnsOne(x => x.Padding).Configure();

            var selectors = kit.OwnsOne(x => x.Selectors);
            selectors.OwnsOne(x => x.Font).Configure();
            selectors.OwnsOne(x => x.ActiveColor);

            var settings = kit.OwnsOne(x => x.Settings);

            var dropdowns = kit.OwnsOne(x => x.Dropdowns);
            dropdowns.OwnsOne(x => x.Font).Configure();
            dropdowns.OwnsOne(x => x.Border).Configure();
            dropdowns.OwnsOne(x => x.Padding).Configure();
        }
    }

    public static class ModelBuilderExtensions
    {
        // Won't need any of this once we go to .NET 6, because EF 6 has implicit ownership
        // https://devblogs.microsoft.com/dotnet/taking-the-ef-core-azure-cosmos-db-provider-for-a-test-drive/
        public static void Configure<T>(this OwnedNavigationBuilder<T, Border> border) where T : class
        {
            border.OwnsOne(x => x.Color);
            border.OwnsOne(x => x.Radius);
            border.OwnsOne(x => x.Width);
        }

        public static void Configure<T>(this OwnedNavigationBuilder<T, Font> font) where T : class
        {
            font.OwnsOne(x => x.Size);
            font.OwnsOne(x => x.LineHeight);
        }

        public static void Configure<T>(this OwnedNavigationBuilder<T, Padding> padding) where T : class
        {
            padding.OwnsOne(x => x.Vertical);
            padding.OwnsOne(x => x.Horizontal);
        }
    }
}
