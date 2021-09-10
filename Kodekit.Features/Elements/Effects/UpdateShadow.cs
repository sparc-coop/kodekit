using Sparc.Core;
using Sparc.Features;
using System.Threading.Tasks;

namespace Kodekit.Features.Elements
{
    public record ShadowsModel(string KitId, Shadow Small, Shadow XLarge);
    public class UpdateShadow : PublicFeature<ShadowsModel, Kit>
    {
        public UpdateShadow(IRepository<Kit> kits)
        {
            Kits = kits;
        }

        public IRepository<Kit> Kits { get; }

        public override async Task<Kit> ExecuteAsync(ShadowsModel request)
        {
            var kit = await Kits.FindAsync(request.KitId);

            kit.UpdateShadows(request.Small, request.XLarge);

            await Kits.UpdateAsync(kit);
            return kit;
        }
    }
}
