using System.Threading.Tasks;
using Sparc.Features;

namespace Kodekit.Features
{
    public class CopyKit : PublicFeature<string, CreateKitResponse>
    {
        public KitRepository Kits { get; }
        public CopyKit(KitRepository kit) => Kits = kit;

        public override async Task<CreateKitResponse> ExecuteAsync(string kitId)
        {
            var kit = await Kits.GetCurrentAsync(kitId);

            var newKit = new Kit(kit.Kit);
            var newRevision = new KitRevision(kit.Revision);

            await Kits.UpdateAsync((newKit, newRevision));

            return new(newKit.Id);
        }
    }
}
