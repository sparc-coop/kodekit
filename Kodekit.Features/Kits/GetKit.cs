using Sparc.Core;
using Sparc.Features;
using System.Threading.Tasks;
using System.Linq;

namespace Kodekit.Features
{
    public class GetKit : PublicFeature<string, Kit>
    {
        public GetKit(KitRepository kits)
        {
            Kits = kits;
        }

        public KitRepository Kits { get; }

        public override async Task<Kit> ExecuteAsync(string kitId) 
        {
            return await Kits.GetKitAsync(kitId);
        }
    }
}
