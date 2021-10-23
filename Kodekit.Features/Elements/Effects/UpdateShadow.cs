using Sparc.Core;
using Sparc.Features;
using System.Threading.Tasks;

namespace Kodekit.Features.Elements
{
    public record ShadowsModel(string KitId, Shadow? Small, Shadow? XLarge);
    public class UpdateShadow : PublicFeature<ShadowsModel, Kit>
    {
        public UpdateShadow(KitRepository kits)
        {
            Kits = kits;
        }

        public KitRepository Kits { get; }

        public override async Task<Kit> ExecuteAsync(ShadowsModel request)
        {
            var kit = await Kits.GetCurrentAsync(request.KitId);

            if (request.Small != null && request.XLarge != null)
                kit.Revision.UpdateShadows(request.Small, request.XLarge);

            await Kits.UpdateAsync(kit);
            return kit.Kit;
        }
    }
}
