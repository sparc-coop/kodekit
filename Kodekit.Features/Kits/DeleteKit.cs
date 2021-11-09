namespace Kodekit.Features.Kits;

public class DeleteKit : Feature<string, bool>
{
    public KitRepository Kits { get; }
    public DeleteKit(KitRepository kits) => Kits = kits;

    public override async Task<bool> ExecuteAsync(string kitId)
    {
        var kit = await Kits.GetKitAsync(kitId);
        if (kit.UserId != User.Id())
            throw new NotAuthorizedException("You do not own this kit!");

        try
        {
            kit.IsDeleted = true;
            await Kits.UpdateAsync(kit);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
