using System.Threading.Tasks;
using Kodekit.Core;
using Sparc.Core;
using Sparc.Features;

namespace Kodekit.Features
{
    public class CreateUpdateKit : PublicFeature<Kit>
    {
        public IRepository<Kit> Kit { get; }
        public CreateUpdateKit(IRepository<Kit> kit) => Kit = kit;

        public override async Task<Kit> ExecuteAsync()
        {
            Kit kit = new Kit();
            kit.Description = "UI Kit";
            await Kit.UpdateAsync(kit);
            return kit;
        }
    }
}
