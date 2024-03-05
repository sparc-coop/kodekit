namespace Kodekit.Models.Elements;

public record UpdateIconsModel(string KitId, string? Name, List<IconLibrary> ValidIcons, List<string> IconsList);
public class UpdateIcons(KitRepository kits) : PublicFeature<UpdateIconsModel, Kit>
{
    public KitRepository Kits { get; } = kits;

    public override async Task<Kit> ExecuteAsync(UpdateIconsModel request)
    {
        var kit = await Kits.GetCurrentAsync(request.KitId);

        var icon = new IconLibrary(request.Name);
        kit.Revision.UpdateIcons(icon);
        await Kits.UpdateAsync(kit);

        return kit.Kit;
    }
}
