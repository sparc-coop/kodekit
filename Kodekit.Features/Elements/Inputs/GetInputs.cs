using Sparc.Core;
using Sparc.Features;
using System.Threading.Tasks;

namespace Kodekit.Features.Elements
{
    public class GetInputs : Feature<string, UpdateInputsModel>
    {
        public GetInputs(IRepository<Kit> kits)
        {
            Kits = kits;
        }

        public IRepository<Kit> Kits { get; }

        public override async Task<UpdateInputsModel> ExecuteAsync(string id)
        {
            var kit = await Kits.FindAsync(id);
            return new(
                id,
                kit.Inputs.FontSize.Value,
                kit.Inputs.FontWeight.Value,
                kit.Inputs.VerticalPadding.Value,
                kit.Inputs.HorizontalPadding.Value,
                kit.Inputs.CornerRadius.Value,
                kit.Inputs.BorderWidth.Value
            );
        }
    }
}
