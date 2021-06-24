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
                //Verify kit can be published
                if (kit.IsPublished == true && string.IsNullOrEmpty(kit.UserId))
                {
                    //return sorry cannot publish a new version unless you sign in.
                    return false;
                }
                else
                {
                    kit.IsPublished = true;
                    kit.ModifiedDate = DateTime.Now;
                    await Kits.UpdateAsync(kit);
                    return true;
                }
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
