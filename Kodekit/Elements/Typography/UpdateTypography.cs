namespace Kodekit.Models.Elements;

public record UpdateTypographyModel(string KitId, TypographyModel Headings, TypographyModel Paragraphs);
public class UpdateTypography(KitRepository kits) : PublicFeature<UpdateTypographyModel, Kit>
{
    public KitRepository Kits { get; } = kits;

    public override async Task<Kit> ExecuteAsync(UpdateTypographyModel request)
    {
        var kit = await Kits.GetCurrentAsync(request.KitId);

        var headings = new Typography(
            request.Headings.FontFamily,
            request.Headings.FontWeight,
            request.Headings.FontSize,
            request.Headings.TypeScale,
            request.Headings.LineHeight,
            request.Headings.FontSizeOverrides);


        var paragraphs = new Typography(
            request.Paragraphs.FontFamily,
            request.Paragraphs.FontWeight,
            request.Paragraphs.FontSize,
            request.Paragraphs.TypeScale,
            request.Paragraphs.LineHeight,
            request.Paragraphs.FontSizeOverrides);


        kit.Revision.UpdateTypography(headings, paragraphs);
        await Kits.UpdateAsync(kit);

        return kit.Kit;
    }
}
