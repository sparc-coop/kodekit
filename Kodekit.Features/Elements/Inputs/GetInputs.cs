using Sparc.Core;
using Sparc.Features;
using System.Threading.Tasks;

namespace Kodekit.Features.Elements
{
    public class GetInputs : PublicFeature<string, UpdateInputsModel>
    {
        public GetInputs(IRepository<Kit> kits)
        {
            Kits = kits;
        }

        public IRepository<Kit> Kits { get; }

        public override async Task<UpdateInputsModel> ExecuteAsync(string id)
        {
            var kit = await Kits.FindAsync(id);
            if (kit == null)
                throw new NotFoundException("Kit not found!");

            return new(
                id,
                kit.Inputs.Font.Size?.Value,
                kit.Inputs.Font.Weight,
                kit.Inputs.Padding.Vertical?.Value,
                kit.Inputs.Padding.Horizontal?.Value,
                kit.Inputs.Border.Radius?.Value,
                kit.Inputs.Border.Width?.Value
            );
        }
    }
}
