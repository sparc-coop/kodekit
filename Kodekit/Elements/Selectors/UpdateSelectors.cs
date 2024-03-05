﻿namespace Kodekit.Models.Elements;

public record UpdateSelectorsModel(string KitId, double? FontSize, string? FontWeight, string? ActiveColor);
public class UpdateSelectors(KitRepository kits) : PublicFeature<UpdateSelectorsModel, Kit>
{
    public KitRepository Kits { get; } = kits;

    public override async Task<Kit> ExecuteAsync(UpdateSelectorsModel request)
    {
        var kit = await Kits.GetCurrentAsync(request.KitId);

        kit.Revision.UpdateSelectors(new Selector(request.FontSize, request.FontWeight, request.ActiveColor));

        await Kits.UpdateAsync(kit);

        return kit.Kit;
    }
}
