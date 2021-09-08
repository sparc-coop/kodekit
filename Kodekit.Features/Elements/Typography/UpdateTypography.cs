using Sparc.Core;
using Sparc.Features;
using System.Threading.Tasks;

namespace Kodekit.Features.Elements
{
    public record UpdateTypographyModel(string KitId, TypographyModel Headings, TypographyModel Paragraphs);
    public class UpdateTypography : Feature<UpdateTypographyModel, Kit>
    {
        public UpdateTypography(IRepository<Kit> kits)
        {
            Kits = kits;
        }

        public IRepository<Kit> Kits { get; }

        public override async Task<Kit> ExecuteAsync(UpdateTypographyModel request)
        {
            var kit = await Kits.FindAsync(request.KitId);

            var headings = new HeadingTypography("h6",
                request.Headings.FontFamily,
                request.Headings.FontWeight,
                request.Headings.FontSize,
                request.Headings.TypeScale,
                request.Headings.LineHeight);


            var paragraphs = new Typography("p",
                request.Paragraphs.FontFamily,
                request.Paragraphs.FontWeight,
                request.Paragraphs.FontSize,
                request.Paragraphs.TypeScale,
                request.Paragraphs.LineHeight);


            kit.UpdateTypography(headings, paragraphs);
            await Kits.UpdateAsync(kit);

            return kit;
        }
    }
}
