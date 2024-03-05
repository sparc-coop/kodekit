﻿namespace Kodekit.Models.Elements;

public record UpdateDropdownsModel(string KitId, double? FontSize, string? FontWeight, double? VerticalPadding, double? HorizontalPadding, double? CornerRadius, double? BorderWidth, bool OverwriteInherited);
public class UpdateDropdowns(KitRepository kits) : PublicFeature<UpdateDropdownsModel, Kit>
{
    public KitRepository Kits { get; } = kits;

    public override async Task<Kit> ExecuteAsync(UpdateDropdownsModel request)
    {
        var kit = await Kits.GetCurrentAsync(request.KitId);

        var Dropdowns = new Dropdown(request.FontSize, request.FontWeight, request.VerticalPadding, request.HorizontalPadding, request.CornerRadius, request.BorderWidth, request.OverwriteInherited);

        kit.Revision.UpdateDropdowns(Dropdowns);
        await Kits.UpdateAsync(kit);

        return kit.Kit;
    }
}
