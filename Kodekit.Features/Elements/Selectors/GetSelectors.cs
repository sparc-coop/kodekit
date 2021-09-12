using Sparc.Core;
using Sparc.Features;
using System.Threading.Tasks;

namespace Kodekit.Features.Elements
{
    public class GetSelectors : PublicFeature<string, UpdateSelectorsModel>
    {
        public GetSelectors(IRepository<Kit> kits)
        {
            Kits = kits;
        }

        public IRepository<Kit> Kits { get; }

        public override async Task<UpdateSelectorsModel> ExecuteAsync(string id)
        {
            var kit = await Kits.FindAsync(id);
            if (kit == null)
                throw new NotFoundException("Kit not found!");

            return new(id, kit.Selectors.Font.Size?.Value, kit.Selectors.Font.Weight, kit.Selectors.ActiveColor?.HexValue);
        }
    }
}
