namespace Kodekit.Features;

public class GetKitCurrentRevision : PublicFeature<string, KitRevision>
{
    public GetKitCurrentRevision(KitRepository kits)
    {
        Kits = kits;
    }

    private readonly KitRepository Kits;

    public override async Task<KitRevision> ExecuteAsync(string kitId)
    {
        var currentRevision = await Kits.GetCurrentRevisionAsync(kitId);
        return currentRevision;
    }
}