namespace Kodekit.Features;

public record UpdateKitRequest(string KitId, string Name, bool IsAutoPublish);
public class UpdateKit : PublicFeature<UpdateKitRequest, Kit>
{
    public KitRepository Kits { get; }
    public UpdateKit(KitRepository kit) => Kits = kit;

    public override async Task<Kit> ExecuteAsync(UpdateKitRequest request)
    {
        var kit = await Kits.GetKitAsync(request.KitId);
        kit.Update(request.Name, request.IsAutoPublish);
        await Kits.UpdateAsync(kit);
        return kit;
    }
}
