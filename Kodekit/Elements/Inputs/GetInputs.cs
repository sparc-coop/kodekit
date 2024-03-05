namespace Kodekit.Models.Elements;

public class GetInputs(KitRepository kits) : PublicFeature<string, UpdateInputsModel>
{
    public KitRepository Kits { get; } = kits;

    public override async Task<UpdateInputsModel> ExecuteAsync(string id)
    {
        var kit = await Kits.GetCurrentRevisionAsync(id);

        return new(
            id,
            kit.Inputs.Font.Size?.Value,
            kit.Inputs.Font.Weight,
            kit.Inputs.Padding.Vertical?.Value,
            kit.Inputs.Padding.Horizontal?.Value,
            kit.Inputs.Border.Radius?.Value,
            kit.Inputs.Border.Width?.Value
        );
    }
}
