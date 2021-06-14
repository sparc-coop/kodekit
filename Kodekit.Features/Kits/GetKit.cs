using Kodekit.Core;
using Sparc.Core;
using Sparc.Features;
using System.Linq;
using System.Threading.Tasks;


namespace Kodekit.Features
{
    public class GetKit : PublicFeature<string, Kit>
    {
        public GetKit(IRepository<Kit> kits)
        {
            Kits = kits;
        }

        public IRepository<Kit> Kits { get; }

        public override async Task<Kit> ExecuteAsync(string kitId) => await Kits.FindAsync(kitId); 
    }
}
