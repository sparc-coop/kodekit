namespace Kodekit.Models;
public class GetAnchors(KitRepository kits) : PublicFeature<string, UpdateAnchorsModel>
{
    public KitRepository Kits { get; } = kits;

    public override async Task<UpdateAnchorsModel> ExecuteAsync(string id)
    {
        var kit = await Kits.GetCurrentRevisionAsync(id);

        return new(
            id,
            kit.Anchors.Font.Size?.Value,
            kit.Anchors.Font.Weight,
            kit.Anchors.DefaultColor?.HexValue,
            kit.Anchors.HoverColor?.HexValue,
            kit.Anchors.VisitedColor?.HexValue,
            kit.Anchors.ActiveColor?.HexValue
        );
    }
}
