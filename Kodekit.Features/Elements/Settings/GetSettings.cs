using Sparc.Core;
using Sparc.Features;
using System.Threading.Tasks;

namespace Kodekit.Features.Elements
{
    public class GetSettings : PublicFeature<string, UpdateSettingsModel>
    {
        public GetSettings(IRepository<Kit> kits)
        {
            Kits = kits;
        }

        public IRepository<Kit> Kits { get; }

        public override async Task<UpdateSettingsModel> ExecuteAsync(string id)
        {
            var kit = await Kits.FindAsync(id);
            return new(id, kit.Settings);
        }
    }
}
