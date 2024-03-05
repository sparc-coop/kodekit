namespace Kodekit;

public class CopyKit(KitRepository kit) : PublicFeature<string, CreateKitResponse>
{
    public KitRepository Kits { get; } = kit;

    public override async Task<CreateKitResponse> ExecuteAsync(string kitId)
    {
        var kit = await Kits.GetCurrentAsync(kitId);

        var newKit = new Kit(Guid.NewGuid().ToString(), kit.Kit);
        var newRevision = new KitRevision(kit.Revision);

        await Kits.UpdateAsync((newKit, newRevision));

        return new(newKit.Id);
    }
}
