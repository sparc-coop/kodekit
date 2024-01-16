namespace Kodekit.Models.Elements;

public class GetDropdowns : PublicFeature<string, UpdateDropdownsModel>
{
    public GetDropdowns(KitRepository kits)
    {
        Kits = kits;
    }

    public KitRepository Kits { get; }

    public override async Task<UpdateDropdownsModel> ExecuteAsync(string id)
    {
        var kit = await Kits.GetCurrentRevisionAsync(id);

        return new(
            id,
            kit.Dropdowns.Font.Size?.Value,
            kit.Dropdowns.Font.Weight,
            kit.Dropdowns.Padding.Vertical?.Value,
            kit.Dropdowns.Padding.Horizontal?.Value,
            kit.Dropdowns.Border.Radius?.Value,
            kit.Dropdowns.Border.Width?.Value,
            kit.Dropdowns.OverwriteInherited
        );
    }
}
