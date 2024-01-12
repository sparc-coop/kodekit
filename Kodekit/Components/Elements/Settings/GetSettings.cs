namespace Kodekit.Features.Components.Elements;

public class GetSettings : PublicFeature<string, UpdateSettingsModel>
{
    public GetSettings(KitRepository kits)
    {
        Kits = kits;
    }

    public KitRepository Kits { get; }

    public override async Task<UpdateSettingsModel> ExecuteAsync(string id)
    {
        var kit = await Kits.GetCurrentRevisionAsync(id);
        return new(id, kit.Settings);
    }
}
