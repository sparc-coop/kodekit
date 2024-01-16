namespace Kodekit.Models.Elements;

public record UpdateIconsModel(string KitId, string? Name, List<IconLibrary> ValidIcons, List<string> IconsList);
public class UpdateIcons : PublicFeature<UpdateIconsModel, Kit>
{
    public UpdateIcons(KitRepository kits)
    {
        Kits = kits;
    }

    public KitRepository Kits { get; }

    public override async Task<Kit> ExecuteAsync(UpdateIconsModel request)
    {
        var kit = await Kits.GetCurrentAsync(request.KitId);

        var icon = new IconLibrary(request.Name);
        kit.Revision.UpdateIcons(icon);
        await Kits.UpdateAsync(kit);

        return kit.Kit;
    }
}
