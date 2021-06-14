using System;
using System.Threading.Tasks;
using Kodekit.Core;
using Sparc.Core;
using Sparc.Features;
using System.Collections.Generic;
using System.Linq;

namespace Kodekit.Features
{
    public class UpdateKit : PublicFeature<Kit, Kit>
    {
        public IRepository<Kit> Kit { get; }
        public UpdateKit(IRepository<Kit> kit) => Kit = kit;

        public override async Task<Kit> ExecuteAsync(Kit kit)
        {
            await Kit.UpdateAsync(kit);
            return kit;
        }
    }
}
