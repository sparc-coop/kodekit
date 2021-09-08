using Sparc.Core;
using Sparc.Features;
using System.Threading.Tasks;

namespace Kodekit.Features.Elements
{
    public class GetDropdowns : Feature<string, UpdateDropdownsModel>
    {
        public GetDropdowns(IRepository<Kit> kits)
        {
            Kits = kits;
        }

        public IRepository<Kit> Kits { get; }

        public override async Task<UpdateDropdownsModel> ExecuteAsync(string id)
        {
            var kit = await Kits.FindAsync(id);
            return new(
                id,
                kit.Dropdowns.FontSize.Value,
                kit.Dropdowns.FontWeight.Value,
                kit.Dropdowns.VerticalPadding.Value,
                kit.Dropdowns.HorizontalPadding.Value,
                kit.Dropdowns.CornerRadius.Value,
                kit.Dropdowns.BorderWidth.Value
            );
        }
    }
}
