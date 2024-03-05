namespace Kodekit.Models.Elements;

public record UpdateSettingsModel(string KitId, KitSettings Settings);
public class UpdateSettings(KitRepository kits) : PublicFeature<UpdateSettingsModel, Kit>
{
    public KitRepository Kits { get; } = kits;

    public override async Task<Kit> ExecuteAsync(UpdateSettingsModel request)
    {
        var kit = await Kits.GetCurrentAsync(request.KitId);

        kit.Revision.UpdateSettings(request.Settings);
        await Kits.UpdateAsync(kit);

        return kit.Kit;
    }
}
