using Sparc.Core;
using Sparc.Features;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kodekit.Features.Elements
{
    public record GetTypographyResponse(TypographyModel Heading, TypographyModel Paragraph, Dictionary<double, string> TypeScales);
    public record TypographyModel(string? FontFamily, string? FontWeight, double? FontSize, double? TypeScale, double? LineHeight);
    public class GetTypography : PublicFeature<string, GetTypographyResponse>
    {
        public GetTypography(IRepository<Kit> kits)
        {
            Kits = kits;
        }

        public IRepository<Kit> Kits { get; }

        public override async Task<GetTypographyResponse> ExecuteAsync(string id)
        {
            var kit = await Kits.FindAsync(id);
            if (kit == null)
                throw new NotFoundException("Kit not found!");


            var heading = new TypographyModel(
                kit.Headings.Font.Family, 
                kit.Headings.Font.Weight,
                kit.Headings.Font.Size?.Value,
                kit.Headings.TypeScale,
                kit.Headings.Font.LineHeight?.Value
            );

            var paragraph = new TypographyModel(
               kit.Paragraphs.Font.Family,
                kit.Paragraphs.Font.Weight,
                kit.Paragraphs.Font.Size?.Value,
                kit.Paragraphs.TypeScale,
                kit.Paragraphs.Font.LineHeight?.Value
            );

            return new(heading, paragraph, Typography.TypeScales);
        }
    }
}
