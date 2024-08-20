using Microsoft.EntityFrameworkCore;

namespace Kodekit;

public partial class KodekitContext(BlossomContextOptions options) : BlossomContext(options)
{
    public BlossomSet<FontWeight> FontWeights = new(Font.ValidWeights);
    public BlossomSet<TypeScale> TypeScales = new(Typography.TypeScales);
    public BlossomSet<GoogleFont> Fonts = BlossomSet<GoogleFont>.FromUrl<GoogleFontResponse>(
            "https://www.googleapis.com/webfonts/v1/webfonts?key=" + options.Configuration["GoogleFontsApiKey"],
            x => x.Items.Where(y => y.Category == "serif" || y.Category != "sans-serif"));


    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<User>().ToContainer("Users").HasPartitionKey(x => x.UserId);

        var kit = builder.Entity<Kit>().HasPartitionKey(x => x.KitId).HasQueryFilter(x => x.UserId == UserId);
        var revision = builder.Entity<KitRevision>().HasPartitionKey(x => x.KitId);
    }
}
