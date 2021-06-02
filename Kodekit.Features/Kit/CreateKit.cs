using System.Threading.Tasks;
using Kodekit.Core;
using Sparc.Core;
using Sparc.Features;

namespace Kodekit.Features
{
    public class CreateKit : PublicFeature<Kit>
    {
        public IRepository<Kit> Kit { get; }
        public CreateKit(IRepository<Kit> kit) => Kit = kit;

        public override async Task<Kit> ExecuteAsync()
        {
            Kit kit = new Kit();
            kit.Description = "New example kit";
            await Kit.UpdateAsync(kit);
            return kit;
        }
    }
}
