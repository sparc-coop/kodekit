namespace Kodekit.Models.Elements;

public record ColorsModel(string KitId, string? Primary, string? Secondary, string? Tertiary, string? Darkest, string? Lightest, string? Error, string? Warning, string? Success);
public class UpdateColors : PublicFeature<ColorsModel, Kit>
{
    public UpdateColors(KitRepository kits)
    {
        Kits = kits;
    }

    public KitRepository Kits { get; }

    public override async Task<Kit> ExecuteAsync(ColorsModel request)
    {
        var kit = await Kits.GetCurrentAsync(request.KitId);
        var revision = kit.Revision;

        revision.UpdateColor(ColorTypes.Primary, request.Primary);
        revision.UpdateColor(ColorTypes.Secondary, request.Secondary);
        revision.UpdateColor(ColorTypes.Tertiary, request.Tertiary);
        revision.UpdateColor(ColorTypes.Error, request.Error);
        revision.UpdateColor(ColorTypes.Warning, request.Warning);
        revision.UpdateColor(ColorTypes.Success, request.Success);
        revision.UpdateColor(ColorTypes.Lightest, request.Lightest);
        revision.UpdateColor(ColorTypes.Darkest, request.Darkest);

        await Kits.UpdateAsync(kit);
        return kit.Kit;
    }
}
