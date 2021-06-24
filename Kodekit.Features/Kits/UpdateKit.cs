using System;
using System.Threading.Tasks;
using Sparc.Core;
using Sparc.Features;

namespace Kodekit.Features
{
    public class UpdateKit : PublicFeature<Kit, Kit>
    {
        public IRepository<Kit> Kit { get; }
        public UpdateKit(IRepository<Kit> kit) => Kit = kit;

        public override async Task<Kit> ExecuteAsync(Kit kit)
        {
            if(kit.DateCreated == null)
            {
                kit.DateCreated = DateTime.Now;
            }
            kit.ModifiedDate = DateTime.Now;
            await Kit.UpdateAsync(kit);
            return kit;
        }
    }
}
