namespace Kodekit.Features.Components.Elements;

public record UpdateSelectorsModel(string KitId, double? FontSize, string? FontWeight, string? ActiveColor);
public class UpdateSelectors : PublicFeature<UpdateSelectorsModel, Kit>
{
    public UpdateSelectors(KitRepository kits)
    {
        Kits = kits;
    }

    public KitRepository Kits { get; }

    public override async Task<Kit> ExecuteAsync(UpdateSelectorsModel request)
    {
        var kit = await Kits.GetCurrentAsync(request.KitId);

        kit.Revision.UpdateSelectors(new Selector(request.FontSize, request.FontWeight, request.ActiveColor));

        await Kits.UpdateAsync(kit);

        return kit.Kit;
    }
}
