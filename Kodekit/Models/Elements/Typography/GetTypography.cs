namespace Kodekit.Models.Elements;

public record GetTypographyResponse(TypographyModel Heading, TypographyModel Paragraph, Dictionary<double, string> TypeScales);
public record TypographyModel(string? FontFamily, string? FontWeight, double? FontSize, double? TypeScale, 
    double? LineHeight, Dictionary<string, string>? FontSizeOverrides, Dictionary<string, string> TypeScaleValues);
public class GetTypography : PublicFeature<string, GetTypographyResponse>
{
    public GetTypography(KitRepository kits)
    {
        Kits = kits;
    }

    public KitRepository Kits { get; }

    public override async Task<GetTypographyResponse> ExecuteAsync(string id)
    {
        var kit = await Kits.GetCurrentRevisionAsync(id);
        if (kit == null)
            throw new NotFoundException("Kit not found!");


        var heading = new TypographyModel(
            kit.Headings.Font.Family,
            kit.Headings.Font.Weight,
            kit.Headings.Font.Size?.Value,
            kit.Headings.TypeScale,
            kit.Headings.Font.LineHeight?.Value,
            kit.Headings.FontSizeOverrides,
            kit.Headings.Serialize() // to show on preview example
        );

        var paragraph = new TypographyModel(
            kit.Paragraphs.Font.Family,
            kit.Paragraphs.Font.Weight,
            kit.Paragraphs.Font.Size?.Value,
            kit.Paragraphs.TypeScale,
            kit.Paragraphs.Font.LineHeight?.Value,
            kit.Paragraphs.FontSizeOverrides,
            kit.Paragraphs.Serialize() // to show on preview example
        );

        return new(heading, paragraph, Typography.TypeScales);
    }
}
