using Sparc.Core;
using Sparc.Features;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using System;

namespace Kodekit.Features
{
    public class PublishKit : PublicFeature<Kit, bool>
    {
        public PublishKit(IRepository<Kit> kits)
        {
            Kits = kits;
        }

        public IRepository<Kit> Kits { get; }

        public override async Task<bool> ExecuteAsync(Kit kit)
        {
            try
            {
                kit.IsPublished = true;
                if(User.Id() != null)
                {
                    kit.UserId = User.Id();
                }
                await Kits.UpdateAsync(kit);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
