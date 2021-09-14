using Sparc.Core;
using Sparc.Features;
using System.Threading.Tasks;

namespace Kodekit.Features.Elements
{
    public record UpdateTypographyModel(string KitId, TypographyModel Headings, TypographyModel Paragraphs);
    public class UpdateTypography : PublicFeature<UpdateTypographyModel, Kit>
    {
        public UpdateTypography(IRepository<Kit> kits)
        {
            Kits = kits;
        }

        public IRepository<Kit> Kits { get; }

        public override async Task<Kit> ExecuteAsync(UpdateTypographyModel request)
        {
            var kit = await Kits.FindAsync(request.KitId);
            if (kit == null)
                throw new NotFoundException("Kit not found!");


            var headings = new Typography(
                request.Headings.FontFamily,
                request.Headings.FontWeight,
                request.Headings.FontSize,
                request.Headings.TypeScale,
                request.Headings.LineHeight);


            var paragraphs = new Typography(
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
