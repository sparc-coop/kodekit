namespace Kodekit.Models.Elements;

public class GetIcons(KitRepository kits) : PublicFeature<string, UpdateIconsModel>
{
    public KitRepository Kits { get; } = kits;

    public override async Task<UpdateIconsModel> ExecuteAsync(string id)
    {
        var kit = await Kits.GetCurrentRevisionAsync(id);

        return new(
            id,
            kit.Icons.Name,
            IconLibrary.GetValidIcons(),
            await kit.Icons.GetRandomIconsAsync()
        );
    }
}
