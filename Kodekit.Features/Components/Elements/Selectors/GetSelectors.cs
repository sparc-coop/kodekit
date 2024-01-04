namespace Kodekit.Features.Components.Elements;

public class GetSelectors : PublicFeature<string, UpdateSelectorsModel>
{
    public GetSelectors(KitRepository kits)
    {
        Kits = kits;
    }

    public KitRepository Kits { get; }

    public override async Task<UpdateSelectorsModel> ExecuteAsync(string id)
    {
        var kit = await Kits.GetCurrentRevisionAsync(id);

        return new(id, kit.Selectors.Font.Size?.Value, kit.Selectors.Font.Weight, kit.Selectors.ActiveColor?.HexValue);
    }
}
