using Sparc.Core;
using Sparc.Features;
using System.Threading.Tasks;

namespace Kodekit.Features.Elements
{
    public class GetColors : PublicFeature<string, ColorsModel>
    {
        public GetColors(IRepository<Kit> kits)
        {
            Kits = kits;
        }

        public IRepository<Kit> Kits { get; }

        public override async Task<ColorsModel> ExecuteAsync(string id)
        {
            var kit = await Kits.FindAsync(id);
            if (kit == null)
                throw new NotFoundException("Kit not found!");

            return new(id,
                kit.GetColor(ColorTypes.Primary)?.HexValue,
                kit.GetColor(ColorTypes.Secondary)?.HexValue,
                kit.GetColor(ColorTypes.Tertiary)?.HexValue,
                kit.GetColor(ColorTypes.Darkest)?.HexValue,
                kit.GetColor(ColorTypes.Lightest)?.HexValue,
                kit.GetColor(ColorTypes.Error)?.HexValue,
                kit.GetColor(ColorTypes.Warning)?.HexValue,
                kit.GetColor(ColorTypes.Success)?.HexValue);
        }
    }
}
