namespace Kodekit.Features.Components.Elements;

public record UpdateAnchorsModel(string KitId, double? FontSize, string? FontWeight, string? DefaultColor, string? HoverColor, string? VisitedColor, string? ActiveColor);
public class UpdateAnchors : PublicFeature<UpdateAnchorsModel, Kit>
{
    public UpdateAnchors(KitRepository kits)
    {
        Kits = kits;
    }

    public KitRepository Kits { get; }

    public override async Task<Kit> ExecuteAsync(UpdateAnchorsModel request)
    {
        var kit = await Kits.GetCurrentAsync(request.KitId);

        var anchor = new Anchor(request.FontSize, request.FontWeight, request.DefaultColor, request.HoverColor, request.VisitedColor, request.ActiveColor);

        kit.Revision.UpdateAnchors(anchor);
        await Kits.UpdateAsync(kit);

        return kit.Kit;
    }
}
