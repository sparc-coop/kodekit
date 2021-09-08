using Sparc.Core;
using Sparc.Features;
using System.Threading.Tasks;

namespace Kodekit.Features.Elements
{
    public class GetSelectors : Feature<string, UpdateSelectorsModel>
    {
        public GetSelectors(IRepository<Kit> kits)
        {
            Kits = kits;
        }

        public IRepository<Kit> Kits { get; }

        public override async Task<UpdateSelectorsModel> ExecuteAsync(string id)
        {
            var kit = await Kits.FindAsync(id);
            return new(id, kit.Selectors);
        }
    }
}
