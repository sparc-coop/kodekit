namespace Kodekit.Features.Elements;

public record UpdateTypographyModel(string KitId, TypographyModel Headings, TypographyModel Paragraphs);
public class UpdateTypography : PublicFeature<UpdateTypographyModel, Kit>
{
    public UpdateTypography(KitRepository kits)
    {
        Kits = kits;
    }

    public KitRepository Kits { get; }

    public override async Task<Kit> ExecuteAsync(UpdateTypographyModel request)
    {
        var kit = await Kits.GetCurrentAsync(request.KitId);

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


        kit.Revision.UpdateTypography(headings, paragraphs);
        await Kits.UpdateAsync(kit);

        return kit.Kit;
    }
}
