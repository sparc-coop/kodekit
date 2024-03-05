﻿namespace Kodekit.Models.Elements;

public record UpdateButtonsModel(string KitId, double? FontSize, string? FontWeight, double? VerticalPadding, double? HorizontalPadding, double? CornerRadius, double? BorderWidth, double? IconWidth, double? IconHeight, bool RemoveSecondaryBorder);
public class UpdateButtons(KitRepository kits) : PublicFeature<UpdateButtonsModel, Kit>
{
    public KitRepository Kits { get; } = kits;

    public override async Task<Kit> ExecuteAsync(UpdateButtonsModel request)
    {
        var kit = await Kits.GetCurrentAsync(request.KitId);

        var buttons = new Button(request.FontSize, request.FontWeight, request.VerticalPadding, request.HorizontalPadding, request.CornerRadius, request.BorderWidth, request.IconWidth, request.IconHeight, request.RemoveSecondaryBorder);

        kit.Revision.UpdateButtons(buttons);
        await Kits.UpdateAsync(kit);

        return kit.Kit;
    }
}
