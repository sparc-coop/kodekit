using Sparc.Core;
using Sparc.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kodekit.Features.Kits
{
    public class DeleteKit : PublicFeature<Kit, bool>
    {
        public IRepository<Kit> Kit { get; }
        public DeleteKit(IRepository<Kit> kit) => Kit = kit;

        public override async Task<bool> ExecuteAsync(Kit kit)
        {
            try
            {
                kit.IsDeleted = true;
                await Kit.UpdateAsync(kit);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
