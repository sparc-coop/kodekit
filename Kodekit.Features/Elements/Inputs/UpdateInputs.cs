namespace Kodekit.Features.Elements;

public record UpdateInputsModel(string KitId, double? FontSize, string? FontWeight, double? VerticalPadding,
    double? HorizontalPadding, double? CornerRadius, double? BorderWidth);
public class UpdateInputs : PublicFeature<UpdateInputsModel, Kit>
{
    public UpdateInputs(KitRepository kits)
    {
        Kits = kits;
    }

    public KitRepository Kits { get; }

    public override async Task<Kit> ExecuteAsync(UpdateInputsModel request)
    {
        var kit = await Kits.GetCurrentAsync(request.KitId);

        var input = new Input(request.FontSize, request.FontWeight, request.VerticalPadding,
            request.HorizontalPadding, request.CornerRadius, request.BorderWidth);

        kit.Revision.UpdateInputs(input);
        await Kits.UpdateAsync(kit);

        return kit.Kit;
    }
}
