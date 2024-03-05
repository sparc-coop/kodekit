namespace Kodekit;

public class SetKitTheme(KitRepository kit) : PublicFeature<string, bool>
{
    public KitRepository Kits { get; } = kit;

    public override async Task<bool> ExecuteAsync(string kitId)
    {
        try
        {
            var kit = await Kits.GetCurrentAsync(kitId);
            //if theme not already set
            if (kit.Kit.ThemeId != null)
                return false;

            kit.Kit.ThemeId = 1;
            kit.Revision = new KitRevision(true, kitId, kit.Kit.CurrentRevisionId);

            await Kits.UpdateAsync(kit);
            return true;

        } catch
        {
            return false;
        }
    }
}
