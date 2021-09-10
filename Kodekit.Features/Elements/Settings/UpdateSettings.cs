using Sparc.Core;
using Sparc.Features;
using System.Threading.Tasks;

namespace Kodekit.Features.Elements
{
    public record UpdateSettingsModel(string KitId, Settings Settings);
    public class UpdateSettings : PublicFeature<UpdateSettingsModel, Kit>
    {
        public UpdateSettings(IRepository<Kit> kits)
        {
            Kits = kits;
        }

        public IRepository<Kit> Kits { get; }

        public override async Task<Kit> ExecuteAsync(UpdateSettingsModel request)
        {
            var kit = await Kits.FindAsync(request.KitId);
            kit.UpdateSettings(request.Settings);
            await Kits.UpdateAsync(kit);

            return kit;
        }
    }
}
