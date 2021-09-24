using System;
using System.Threading.Tasks;
using Sparc.Core;
using Sparc.Features;

namespace Kodekit.Features
{
    public class CopyKit : PublicFeature<string, CreateKitResponse>
    {
        public IRepository<Kit> Kits { get; }
        public CopyKit(IRepository<Kit> kit) => Kits = kit;

        public override async Task<CreateKitResponse> ExecuteAsync(string kitId)
        {
            var kit = await Kits.FindAsync(kitId);
            if (kit == null)
                throw new NotFoundException("Couldn't find kit!");

            kit.Id = Guid.NewGuid().ToString();
            //kit.UserId = User.Id();
            kit.DateCreated = DateTime.Now;
            kit.ParentId = kitId;

            await Kits.AddAsync(kit);
            
            return new(kit.Id);
        }
    }
}
