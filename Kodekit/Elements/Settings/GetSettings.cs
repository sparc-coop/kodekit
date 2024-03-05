namespace Kodekit.Models.Elements;

public class GetSettings(KitRepository kits) : PublicFeature<string, UpdateSettingsModel>
{
    public KitRepository Kits { get; } = kits;

    public override async Task<UpdateSettingsModel> ExecuteAsync(string id)
    {
        var kit = await Kits.GetCurrentRevisionAsync(id);
        return new(id, kit.Settings);
    }
}
