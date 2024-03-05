namespace Kodekit;

public record UpdateKitRequest(string KitId, string Name, string? UserId = null, bool IsAutoPublish = false);
public class UpdateKit : PublicFeature<UpdateKitRequest, Kit>
{
    public KitRepository Kits { get; }
    public UpdateKit(KitRepository kit) => Kits = kit;

    public override async Task<Kit> ExecuteAsync(UpdateKitRequest request)
    {
        var kit = await Kits.GetKitAsync(request.KitId);
        
        if (string.IsNullOrWhiteSpace(kit.UserId) && User?.Id() != null)
            kit.SetUser(User.Id());

        kit.Update(request.Name, request.IsAutoPublish);
        await Kits.UpdateAsync((kit, null));
        return kit;
    }
}