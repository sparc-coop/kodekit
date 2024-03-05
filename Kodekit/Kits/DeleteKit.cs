using Sparc.Blossom.Authentication;

namespace Kodekit.Models.Kits;

public class DeleteKit(KitRepository kits) : Feature<string, bool>
{
    public KitRepository Kits { get; } = kits;

    public override async Task<bool> ExecuteAsync(string kitId)
    {
        var kit = await Kits.GetKitAsync(kitId);
        if (kit.UserId != User.Id())
            throw new NotAuthorizedException("You do not own this kit!");

        try
        {
            kit.IsDeleted = true;
            await Kits.UpdateAsync((kit, null));
            return true;
        }
        catch
        {
            return false;
        }
    }
}
