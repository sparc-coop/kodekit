namespace Kodekit.Features.Elements;

public record UpdateDropdownsModel(string KitId, double? FontSize, string? FontWeight, double? VerticalPadding, double? HorizontalPadding, double? CornerRadius, double? BorderWidth, bool OverwriteInherited);
public class UpdateDropdowns : PublicFeature<UpdateDropdownsModel, Kit>
{
    public UpdateDropdowns(KitRepository kits)
    {
        Kits = kits;
    }

    public KitRepository Kits { get; }

    public override async Task<Kit> ExecuteAsync(UpdateDropdownsModel request)
    {
        var kit = await Kits.GetCurrentAsync(request.KitId);

        var Dropdowns = new Dropdown(request.FontSize, request.FontWeight, request.VerticalPadding, request.HorizontalPadding, request.CornerRadius, request.BorderWidth, request.OverwriteInherited);

        kit.Revision.UpdateDropdowns(Dropdowns);
        await Kits.UpdateAsync(kit);

        return kit.Kit;
    }
}
