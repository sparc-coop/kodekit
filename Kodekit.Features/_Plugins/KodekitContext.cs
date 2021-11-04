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
            
            var kit = builder.Entity<Kit>().HasPartitionKey(x => x.KitId);
            var revision = builder.Entity<KitRevision>().HasPartitionKey(x => x.KitId);

            var colors = revision.OwnsMany(x => x.Colors);
            colors.OwnsOne(x => x.Value);

            var shadows = revision.OwnsMany(x => x.Shadows);
            shadows.OwnsOne(x => x.Value);

            var headings = revision.OwnsOne(x => x.Headings);
            headings.OwnsOne(x => x.Font).Configure();

            var paragraphs = revision.OwnsOne(x => x.Paragraphs);
            paragraphs.OwnsOne(x => x.Font).Configure();

            var buttons = revision.OwnsOne(x => x.Buttons);
            buttons.OwnsOne(x => x.Font).Configure();
            buttons.OwnsOne(x => x.Border).Configure();

            buttons.OwnsOne(x => x.Padding).Configure();
            buttons.OwnsOne(x => x.IconHeight);
            buttons.OwnsOne(x => x.IconWidth);

            var inputs = revision.OwnsOne(x => x.Inputs);
            inputs.OwnsOne(x => x.Font).Configure();
            inputs.OwnsOne(x => x.Border).Configure();
            inputs.OwnsOne(x => x.Padding).Configure();

            var selectors = revision.OwnsOne(x => x.Selectors);
            selectors.OwnsOne(x => x.Font).Configure();
            selectors.OwnsOne(x => x.ActiveColor);

            var settings = revision.OwnsOne(x => x.Settings);

            var dropdowns = revision.OwnsOne(x => x.Dropdowns);
            dropdowns.OwnsOne(x => x.Font).Configure();
            dropdowns.OwnsOne(x => x.Border).Configure();
            dropdowns.OwnsOne(x => x.Padding).Configure();

            var anchors = revision.OwnsOne(x => x.Anchors);
            anchors.OwnsOne(x => x.Font).Configure();
            anchors.OwnsOne(x => x.DefaultColor);
            anchors.OwnsOne(x => x.HoverColor);
            anchors.OwnsOne(x => x.VisitedColor);
            anchors.OwnsOne(x => x.ActiveColor);

            var lists = revision.OwnsOne(x => x.Lists);
            lists.OwnsOne(x => x.Font).Configure();
            lists.OwnsOne(x => x.ItemPadding).Configure();
            lists.OwnsOne(x => x.ListPadding).Configure();

            revision.OwnsOne(x => x.Icons);
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
