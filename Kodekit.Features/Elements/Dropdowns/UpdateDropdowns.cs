using Sparc.Core;
using Sparc.Features;
using System.Threading.Tasks;

namespace Kodekit.Features.Elements
{
    public record UpdateDropdownsModel(string KitId, double? FontSize, string? FontWeight, double? VerticalPadding, double? HorizontalPadding, double? CornerRadius, double? BorderWidth, bool OverwriteInherited);
    public class UpdateDropdowns : PublicFeature<UpdateDropdownsModel, Kit>
    {
        public UpdateDropdowns(IRepository<Kit> kits)
        {
            Kits = kits;
        }

        public IRepository<Kit> Kits { get; }

        public override async Task<Kit> ExecuteAsync(UpdateDropdownsModel request)
        {
            var kit = await Kits.FindAsync(request.KitId);
            if (kit == null)
                throw new NotFoundException("Kit not found!");

            var Dropdowns = new Dropdown(request.FontSize, request.FontWeight, request.VerticalPadding, request.HorizontalPadding, request.CornerRadius, request.BorderWidth, request.OverwriteInherited);

            kit.UpdateDropdowns(Dropdowns);
            await Kits.UpdateAsync(kit);

            return kit;
        }
    }
}
