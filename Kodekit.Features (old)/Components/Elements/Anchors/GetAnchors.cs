namespace Kodekit.Features.Components.Elements;

public class GetAnchors : PublicFeature<string, UpdateAnchorsModel>
{
    public GetAnchors(KitRepository kits)
    {
        Kits = kits;
    }

    public KitRepository Kits { get; }

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
