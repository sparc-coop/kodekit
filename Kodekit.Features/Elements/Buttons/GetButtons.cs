namespace Kodekit.Features.Elements;

public class GetButtons : PublicFeature<string, UpdateButtonsModel>
{
    public GetButtons(KitRepository kits)
    {
        Kits = kits;
    }

    public KitRepository Kits { get; }

    public override async Task<UpdateButtonsModel> ExecuteAsync(string id)
    {
        var kit = await Kits.GetCurrentRevisionAsync(id);

        return new(
            id,
            kit.Buttons.Font.Size?.Value,
            kit.Buttons.Font.Weight,
            kit.Buttons.Padding.Vertical?.Value,
            kit.Buttons.Padding.Horizontal?.Value,
            kit.Buttons.Border.Radius?.Value,
            kit.Buttons.Border.Width?.Value,
            kit.Buttons.IconWidth?.Value,
            kit.Buttons.IconHeight?.Value,
            kit.Buttons.RemoveSecondaryBorder
        );
    }
}
