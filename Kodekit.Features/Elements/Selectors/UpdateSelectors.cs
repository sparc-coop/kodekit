using Sparc.Core;
using Sparc.Features;
using System.Threading.Tasks;

namespace Kodekit.Features.Elements
{
    public record UpdateSelectorsModel(string KitId, Selector Selectors);
    public class UpdateSelectors : Feature<UpdateSelectorsModel, Kit>
    {
        public UpdateSelectors(IRepository<Kit> kits)
        {
            Kits = kits;
        }

        public IRepository<Kit> Kits { get; }

        public override async Task<Kit> ExecuteAsync(UpdateSelectorsModel request)
        {
            var kit = await Kits.FindAsync(request.KitId);
            kit.UpdateSelectors(request.Selectors);
            await Kits.UpdateAsync(kit);

            return kit;
        }
    }
}
