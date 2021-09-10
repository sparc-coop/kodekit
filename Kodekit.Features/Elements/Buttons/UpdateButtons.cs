using Sparc.Core;
using Sparc.Features;
using System.Threading.Tasks;

namespace Kodekit.Features.Elements
{
    public record UpdateButtonsModel(string KitId, double FontSize, string FontWeight, double VerticalPadding, double HorizontalPadding, double CornerRadius, double BorderWidth, double IconWidth, double IconHeight, bool RemoveSecondaryBorder);
    public class UpdateButtons : PublicFeature<UpdateButtonsModel, Kit>
    {
        public UpdateButtons(IRepository<Kit> kits)
        {
            Kits = kits;
        }

        public IRepository<Kit> Kits { get; }

        public override async Task<Kit> ExecuteAsync(UpdateButtonsModel request)
        {
            var kit = await Kits.FindAsync(request.KitId);

            var buttons = new Button(request.FontSize, request.FontWeight, request.VerticalPadding, request.HorizontalPadding, request.CornerRadius, request.BorderWidth, request.IconWidth, request.IconHeight, request.RemoveSecondaryBorder);

            kit.UpdateButtons(buttons);
            await Kits.UpdateAsync(kit);

            return kit;
        }
    }
}
