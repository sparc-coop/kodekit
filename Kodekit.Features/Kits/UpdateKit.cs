using System;
using System.Threading.Tasks;
using Sparc.Core;
using Sparc.Features;

namespace Kodekit.Features
{
    public record UpdateKitRequest(string KitId, string Name);
    public class UpdateKit : PublicFeature<UpdateKitRequest, Kit>
    {
        public KitRepository Kits { get; }
        public UpdateKit(KitRepository kit) => Kits = kit;

        public override async Task<Kit> ExecuteAsync(UpdateKitRequest request)
        {
            var kit = await Kits.GetKitAsync(request.KitId);
            kit.UpdateName(request.Name);
            await Kits.UpdateAsync(kit);
            return kit;
        }
    }
}
