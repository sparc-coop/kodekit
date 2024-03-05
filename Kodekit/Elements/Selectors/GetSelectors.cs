namespace Kodekit.Models.Elements;

public class GetSelectors(KitRepository kits) : PublicFeature<string, UpdateSelectorsModel>
{
    public KitRepository Kits { get; } = kits;

    public override async Task<UpdateSelectorsModel> ExecuteAsync(string id)
    {
        var kit = await Kits.GetCurrentRevisionAsync(id);

        return new(id, kit.Selectors.Font.Size?.Value, kit.Selectors.Font.Weight, kit.Selectors.ActiveColor?.HexValue);
    }
}
