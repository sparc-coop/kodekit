namespace Kodekit.Models.Elements;

public record UpdateListsModel(string KitId, double? FontSize, string? FontWeight, string? OlStyleType, string? UlStyleType,
    double? ListHorizontalPadding, double? ListVerticalPadding,
    double? ItemHorizontalPadding, double? ItemVerticalPadding);
public class UpdateLists(KitRepository kits) : PublicFeature<UpdateListsModel, Kit>
{
    public KitRepository Kits { get; } = kits;

    public override async Task<Kit> ExecuteAsync(UpdateListsModel request)
    {
        var kit = await Kits.GetCurrentAsync(request.KitId);

        var List = new List(request.FontSize, request.FontWeight, request.OlStyleType, request.UlStyleType,
            request.ListHorizontalPadding, request.ListVerticalPadding,
            request.ItemHorizontalPadding, request.ItemVerticalPadding);

        kit.Revision.UpdateLists(List);
        await Kits.UpdateAsync(kit);

        return kit.Kit;
    }
}
