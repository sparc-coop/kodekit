using Sparc.Core;
using Sparc.Features;
using System.Threading.Tasks;

namespace Kodekit.Features.Elements
{
    public record UpdateSettingsModel(string KitId, KitSettings Settings);
    public class UpdateSettings : PublicFeature<UpdateSettingsModel, Kit>
    {
        public UpdateSettings(KitRepository kits)
        {
            Kits = kits;
        }

        public KitRepository Kits { get; }

        public override async Task<Kit> ExecuteAsync(UpdateSettingsModel request)
        {
            var kit = await Kits.GetCurrentAsync(request.KitId);

            kit.Revision.UpdateSettings(request.Settings);
            await Kits.UpdateAsync(kit);

            return kit.Kit;
        }
    }
}
