namespace Kodekit;

public class GetKit(KitRepository kits) : PublicFeature<string, Kit>
{
    public KitRepository Kits { get; } = kits;

    public override async Task<Kit> ExecuteAsync(string kitId)
    {
        return await Kits.GetKitAsync(kitId);
    }
}
