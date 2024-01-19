namespace Kodekit;

public class CopyKit : PublicFeature<string, CreateKitResponse>
{
    public KitRepository Kits { get; }
    public CopyKit(KitRepository kit) => Kits = kit;

    public override async Task<CreateKitResponse> ExecuteAsync(string kitId)
    {
        var kit = await Kits.GetCurrentAsync(kitId);

        var newKit = new Kit(Guid.NewGuid().ToString(), kit.Kit);
        var newRevision = new KitRevision(kit.Revision);

        await Kits.UpdateAsync((newKit, newRevision));

        return new(newKit.Id);
    }
}
