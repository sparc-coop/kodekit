using System;
using System.Threading.Tasks;
using Kodekit.Core;
using Sparc.Core;
using Sparc.Features;
using System.Collections.Generic;
using System.Linq;

namespace Kodekit.Features
{
    public class CreateUpdateKit : PublicFeature<Kit, Kit>
    {
        public IRepository<Kit> Kit { get; }
        public CreateUpdateKit(IRepository<Kit> kit) => Kit = kit;

        public override async Task<Kit> ExecuteAsync(Kit kit)
        {
            kit.DateCreated = DateTime.Now;
            await Kit.AddAsync(kit);
            return kit;
        }
    }
}
