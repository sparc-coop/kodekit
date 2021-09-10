using Sparc.Core;
using Sparc.Features;
using System.Threading.Tasks;

namespace Kodekit.Features.Elements
{
    public record UpdateSelectorsModel(string KitId, double FontSize, string FontWeight, string ActiveColor);
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
            kit.UpdateSelectors(new Selector(request.FontSize, request.FontWeight, request.ActiveColor));
            await Kits.UpdateAsync(kit);

            return kit;
        }
    }
}
