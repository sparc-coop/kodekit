using Sparc.Core;
using Sparc.Features;
using System.Threading.Tasks;

namespace Kodekit.Features.Elements
{
    public class GetButtons : Feature<string, UpdateButtonsModel>
    {
        public GetButtons(IRepository<Kit> kits)
        {
            Kits = kits;
        }

        public IRepository<Kit> Kits { get; }

        public override async Task<UpdateButtonsModel> ExecuteAsync(string id)
        {
            var kit = await Kits.FindAsync(id);
            return new(
                id,
                kit.Buttons.FontSize.Value,
                kit.Buttons.FontWeight.Value,
                kit.Buttons.VerticalPadding.Value,
                kit.Buttons.HorizontalPadding.Value,
                kit.Buttons.CornerRadius.Value,
                kit.Buttons.BorderWidth.Value,
                kit.Buttons.IconWidth.Value,
                kit.Buttons.IconHeight.Value,
                kit.Buttons.RemoveSecondaryBorder
            );
        }
    }
}
