namespace Kodekit.Models.Elements;

public record ShadowsModel(string KitId, Shadow? Small, Shadow? XLarge);
public class UpdateShadow(KitRepository kits) : PublicFeature<ShadowsModel, Kit>
{
    public KitRepository Kits { get; } = kits;

    public override async Task<Kit> ExecuteAsync(ShadowsModel request)
    {
        var kit = await Kits.GetCurrentAsync(request.KitId);

        if (request.Small != null && request.XLarge != null)
            kit.Revision.UpdateShadows(request.Small, request.XLarge);

        await Kits.UpdateAsync(kit);
        return kit.Kit;
    }
}
