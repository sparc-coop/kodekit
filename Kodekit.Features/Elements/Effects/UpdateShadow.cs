using Sparc.Core;
using Sparc.Features;
using System.Threading.Tasks;

namespace Kodekit.Features.Elements
{
    public record ShadowsModel(string KitId, ShadowModel Small, ShadowModel XLarge);
    public record ShadowModel(double X, double Y, double Blur, double Spread, string Color, double Opacity);
    public class UpdateShadow : Feature<ShadowsModel, Kit>
    {
        public UpdateShadow(IRepository<Kit> kits)
        {
            Kits = kits;
        }

        public IRepository<Kit> Kits { get; }

        public override async Task<Kit> ExecuteAsync(ShadowsModel request)
        {
            var kit = await Kits.FindAsync(request.KitId);

            var smallShadow = new Shadow(request.Small.X, request.Small.Y, request.Small.Blur, request.Small.Spread, request.Small.Color, request.Small.Opacity);
            var xLargeShadow = new Shadow(request.XLarge.X, request.XLarge.Y, request.XLarge.Blur, request.XLarge.Spread, request.XLarge.Color, request.XLarge.Opacity);

            kit.UpdateShadows(smallShadow, xLargeShadow);

            await Kits.UpdateAsync(kit);
            return kit;
        }
    }
}
