using Sparc.Core;
using Sparc.Features;
using System.Threading.Tasks;

namespace Kodekit.Features.Elements
{
    public record ColorsModel(string KitId, string Primary, string Secondary, string Tertiary, string Darkest, string Lightest, string Error, string Warning, string Success);
    public class UpdateColors : PublicFeature<ColorsModel, Kit>
    {
        public UpdateColors(IRepository<Kit> kits)
        {
            Kits = kits;
        }

        public IRepository<Kit> Kits { get; }

        public override async Task<Kit> ExecuteAsync(ColorsModel request)
        {
            var kit = await Kits.FindAsync(request.KitId);

            kit.UpdateColor(ColorTypes.Primary, request.Primary);
            kit.UpdateColor(ColorTypes.Secondary, request.Secondary);
            kit.UpdateColor(ColorTypes.Tertiary, request.Tertiary);
            kit.UpdateColor(ColorTypes.Error, request.Error);
            kit.UpdateColor(ColorTypes.Warning, request.Warning);
            kit.UpdateColor(ColorTypes.Success, request.Success);
            kit.UpdateColor(ColorTypes.Lightest, request.Lightest);
            kit.UpdateColor(ColorTypes.Darkest, request.Darkest);

            await Kits.UpdateAsync(kit);
            return kit;
        }
    }
}
