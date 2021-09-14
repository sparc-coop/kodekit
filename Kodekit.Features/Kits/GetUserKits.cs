using Sparc.Core;
using Sparc.Features;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kodekit.Features
{
    public class GetUserKits : Feature<List<Kit>>
    {
        public GetUserKits(IRepository<Kit> kits)
        {
            Kits = kits;
        }

        public IRepository<Kit> Kits { get; }

        public override async Task<List<Kit>> ExecuteAsync()
        {
            var kits = await Kits.Query.Where(x => x.UserId == User.Id() && x.ParentId == null && x.IsDeleted != true)
                .ToListAsync();
            return kits;
        }
    }
}
