using Sparc.Core;
using Sparc.Features;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Kodekit.Features.Elements
{
    public record GetTypographyResponse(TypographyModel Heading, TypographyModel Paragraph, Dictionary<double, string> TypeScales);
    public record TypographyModel(string FontFamily, string FontWeight, double? FontSize, double? TypeScale, double? LineHeight);
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

            var heading = new TypographyModel(
                kit.Headings.FontFamily?.InternalName, 
                kit.Headings.FontWeight?.Value,
                kit.Headings.FontSize?.Value,
                kit.Headings.TypeScale?.Value,
                kit.Headings.LineHeight?.Value
            );

            var paragraph = new TypographyModel(
                kit.Paragraphs.FontFamily?.InternalName,
                kit.Paragraphs.FontWeight?.Value,
                kit.Paragraphs.FontSize?.Value,
                kit.Paragraphs.TypeScale?.Value,
                kit.Paragraphs.LineHeight?.Value
            );

            return new(heading, paragraph, TypeScale.ValidValues);
        }
    }
}
