using Sparc.Core;
using Sparc.Features;
using System.Threading.Tasks;
using System.Linq;

namespace Kodekit.Features
{
    public class GetKit : PublicFeature<string, Kit>
    {
        public GetKit(IRepository<Kit> kits)
        {
            Kits = kits;
        }

        public IRepository<Kit> Kits { get; }

        public override async Task<Kit> ExecuteAsync(string kitId) 
        {
            Kit kit = await Kits.FindAsync(kitId);
            if (kit == null || (string.IsNullOrEmpty(kit.UserId) && kit.IsPublished == true))
            {
                kit = new Kit();
            }
            return kit;
        }
    }
}
