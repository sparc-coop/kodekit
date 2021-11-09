namespace Kodekit.Features.Kits;

public class DeleteKit : PublicFeature<Kit, bool>
{
    public IRepository<Kit> Kit { get; }
    public DeleteKit(IRepository<Kit> kit) => Kit = kit;

    public override async Task<bool> ExecuteAsync(Kit kit)
    {
        try
        {
            kit.IsDeleted = true;
            await Kit.UpdateAsync(kit);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
