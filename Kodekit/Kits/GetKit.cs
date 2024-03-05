namespace Kodekit;

public class GetKit : PublicFeature<string, Kit>
{
    public GetKit(KitRepository kits)
    {
        Kits = kits;
    }

    public KitRepository Kits { get; }

    public override async Task<Kit> ExecuteAsync(string kitId)
    {
        return await Kits.GetKitAsync(kitId);
    }
}
