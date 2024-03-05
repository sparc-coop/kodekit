namespace Kodekit.Models.Elements;

public record UpdateAnchorsModel(string KitId, double? FontSize, string? FontWeight, string? DefaultColor, string? HoverColor, string? VisitedColor, string? ActiveColor);
public class UpdateAnchors(KitRepository kits) : PublicFeature<UpdateAnchorsModel, Kit>
{
    public KitRepository Kits { get; } = kits;

    public override async Task<Kit> ExecuteAsync(UpdateAnchorsModel request)
    {
        var kit = await Kits.GetCurrentAsync(request.KitId);

        var anchor = new Anchor(request.FontSize, request.FontWeight, request.DefaultColor, request.HoverColor, request.VisitedColor, request.ActiveColor);

        kit.Revision.UpdateAnchors(anchor);
        await Kits.UpdateAsync(kit);

        return kit.Kit;
    }
}
