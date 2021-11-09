namespace Kodekit.Features.Elements;

public class GetIcons : PublicFeature<string, UpdateIconsModel>
{
    public GetIcons(KitRepository kits)
    {
        Kits = kits;
    }

    public KitRepository Kits { get; }

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
