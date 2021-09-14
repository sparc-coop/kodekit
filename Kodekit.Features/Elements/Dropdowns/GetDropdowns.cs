using Sparc.Core;
using Sparc.Features;
using System.Threading.Tasks;

namespace Kodekit.Features.Elements
{
    public class GetDropdowns : PublicFeature<string, UpdateDropdownsModel>
    {
        public GetDropdowns(IRepository<Kit> kits)
        {
            Kits = kits;
        }

        public IRepository<Kit> Kits { get; }

        public override async Task<UpdateDropdownsModel> ExecuteAsync(string id)
        {
            var kit = await Kits.FindAsync(id);
            if (kit == null)
                throw new NotFoundException("Kit not found!");

            return new(
                id,
                kit.Dropdowns.Font.Size?.Value,
                kit.Dropdowns.Font.Weight,
                kit.Dropdowns.Padding.Vertical?.Value,
                kit.Dropdowns.Padding.Horizontal?.Value,
                kit.Dropdowns.Border.Radius?.Value,
                kit.Dropdowns.Border.Width?.Value,
                kit.Dropdowns.OverwriteInherited
            );
        }
    }
}
