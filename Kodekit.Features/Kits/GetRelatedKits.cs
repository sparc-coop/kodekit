﻿using Sparc.Core;
using Sparc.Features;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace Kodekit.Features
{
    public class GetRelatedKits : Feature<string, List<Kit>>
    {
        public GetRelatedKits(IRepository<Kit> kits)
        {
            Kits = kits;
        }

        public IRepository<Kit> Kits { get; }

        public override async Task<List<Kit>> ExecuteAsync(string kitId)
        {
            return await Kits.Query.Where(x => x.UserId == User.Id() && (x.ParentId == kitId || x.KitId == kitId))
                .OrderBy(x => x.DateCreated)
                .ToListAsync();
        }

    }
}
