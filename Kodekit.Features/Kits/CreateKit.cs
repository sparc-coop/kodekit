using System;
using System.Threading.Tasks;
using Sparc.Core;
using Sparc.Features;

namespace Kodekit.Features
{
    public class CreateKit : PublicFeature<Kit, Kit>
    {
        public IRepository<Kit> Kit { get; }
        public CreateKit(IRepository<Kit> kit) => Kit = kit;

        public override async Task<Kit> ExecuteAsync(Kit kit)
        {
            kit.DateCreated = DateTime.Now;
            await Kit.AddAsync(kit);
            return kit;
        }
    }
}
