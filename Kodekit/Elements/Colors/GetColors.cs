namespace Kodekit.Models.Elements;

public class GetColors(KitRepository kits) : PublicFeature<string, ColorsModel>
{
    public KitRepository Kits { get; } = kits;

    public override async Task<ColorsModel> ExecuteAsync(string id)
    {
        var kit = await Kits.GetCurrentRevisionAsync(id);

        return new(id,
            kit.GetColor(ColorTypes.Primary)?.HexValue,
            kit.GetColor(ColorTypes.Secondary)?.HexValue,
            kit.GetColor(ColorTypes.Tertiary)?.HexValue,
            kit.GetColor(ColorTypes.Darkest)?.HexValue,
            kit.GetColor(ColorTypes.Lightest)?.HexValue,
            kit.GetColor(ColorTypes.Error)?.HexValue,
            kit.GetColor(ColorTypes.Warning)?.HexValue,
            kit.GetColor(ColorTypes.Success)?.HexValue);
    }
}
