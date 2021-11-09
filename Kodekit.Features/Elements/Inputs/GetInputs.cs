namespace Kodekit.Features.Elements;

public class GetInputs : PublicFeature<string, UpdateInputsModel>
{
    public GetInputs(KitRepository kits)
    {
        Kits = kits;
    }

    public KitRepository Kits { get; }

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
