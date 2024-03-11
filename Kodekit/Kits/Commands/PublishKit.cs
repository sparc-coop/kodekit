namespace Kodekit;

public class PublishKit(IRepository<Kit> kits) : PublicFeature<Kit, bool>
{
    public IRepository<Kit> Kits { get; } = kits;

    public override async Task<bool> ExecuteAsync(Kit kit)
    {
        try
        {
            //Verify kit can be published
            if (kit.PublishedRevisionId != null && string.IsNullOrEmpty(kit.UserId))
            {
                //return sorry cannot publish a new version unless you sign in.
                return false;
            }
            else
            {
                kit.Publish();
                await Kits.UpdateAsync(kit);
                return true;
            }
        }
        catch
        {
            return false;
        }
    }
}
