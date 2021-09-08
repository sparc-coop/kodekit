using Sparc.Core;
using Sparc.Features;
using System.Threading.Tasks;

namespace Kodekit.Features.Elements
{
    public class GetShadow : Feature<string, ShadowsModel>
    {
        public GetShadow(IRepository<Kit> kits)
        {
            Kits = kits;
        }

        public IRepository<Kit> Kits { get; }

        public override async Task<ShadowsModel> ExecuteAsync(string id)
        {
            var kit = await Kits.FindAsync(id);
            
            var smallShadow = kit.GetShadow("small");
            var xlargeShadow = kit.GetShadow("xlarge");

            var smallShadowModel = new ShadowModel(smallShadow.X, smallShadow.Y, smallShadow.Blur, smallShadow.Spread, smallShadow.Color, smallShadow.Opacity);
            var xlargeShadowModel = new ShadowModel(xlargeShadow.X, xlargeShadow.Y, xlargeShadow.Blur, xlargeShadow.Spread, xlargeShadow.Color, xlargeShadow.Opacity);

            return new(id, smallShadowModel, xlargeShadowModel);
        }
    }
}
