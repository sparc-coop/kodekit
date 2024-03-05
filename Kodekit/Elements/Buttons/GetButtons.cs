namespace Kodekit.Models.Elements;

public class GetButtons(KitRepository kits) : PublicFeature<string, UpdateButtonsModel>
{
    public KitRepository Kits { get; } = kits;

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
