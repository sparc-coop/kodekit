using Sparc.Core;
using Sparc.Features;
using System.Threading.Tasks;

namespace Kodekit.Features.Elements
{
    public class GetShadow : PublicFeature<string, ShadowsModel>
    {
        public GetShadow(IRepository<Kit> kits)
        {
            Kits = kits;
        }

        public IRepository<Kit> Kits { get; }

        public override async Task<ShadowsModel> ExecuteAsync(string id)
        {
            var kit = await Kits.FindAsync(id);
            if (kit == null)
                throw new NotFoundException("Kit not found!");

            var smallShadow = kit.GetShadow("small");
            var xlargeShadow = kit.GetShadow("xlarge");

            return new(id, smallShadow, xlargeShadow);
        }
    }
}
