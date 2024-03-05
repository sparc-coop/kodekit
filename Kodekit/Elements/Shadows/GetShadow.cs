namespace Kodekit.Models.Elements;

public class GetShadow(KitRepository kits) : PublicFeature<string, ShadowsModel>
{
    public KitRepository Kits { get; } = kits;

    public override async Task<ShadowsModel> ExecuteAsync(string id)
    {
        var kit = await Kits.GetCurrentRevisionAsync(id);
        if (kit == null)
            throw new NotFoundException("Kit not found!");

        var smallShadow = kit.GetShadow("small");
        var xlargeShadow = kit.GetShadow("xlarge");

        return new(id, smallShadow ?? new Shadow(), xlargeShadow ?? new Shadow());
    }
}
