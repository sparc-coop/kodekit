namespace Kodekit.Features.Elements;

public class GetColors : PublicFeature<string, ColorsModel>
{
    public GetColors(KitRepository kits)
    {
        Kits = kits;
    }

    public KitRepository Kits { get; }

    public override async Task<ColorsModel> ExecuteAsync(string id)
    {
        var kit = await Kits.GetCurrentRevisionAsync(id);
        GenerateInitialColorOverrides(kit.GetColor(ColorTypes.Primary));
        GenerateInitialColorOverrides(kit.GetColor(ColorTypes.Secondary));
        GenerateInitialColorOverrides(kit.GetColor(ColorTypes.Tertiary));

        return new(id,
            kit.GetColor(ColorTypes.Primary)?.HexValue,
            kit.GetColor(ColorTypes.Secondary)?.HexValue,
            kit.GetColor(ColorTypes.Tertiary)?.HexValue,
            kit.GetColor(ColorTypes.Darkest)?.HexValue,
            kit.GetColor(ColorTypes.Lightest)?.HexValue,
            kit.GetColor(ColorTypes.Error)?.HexValue,
            kit.GetColor(ColorTypes.Warning)?.HexValue,
            kit.GetColor(ColorTypes.Success)?.HexValue,
            kit.GetColor(ColorTypes.Primary)?.ColorOverrides,
            kit.GetColor(ColorTypes.Secondary)?.ColorOverrides,
            kit.GetColor(ColorTypes.Tertiary)?.ColorOverrides
            );
    }

    private void GenerateInitialColorOverrides(Color? color)
    {
        if (color == null) return;
        color.CalculateGradients();
    }
}
