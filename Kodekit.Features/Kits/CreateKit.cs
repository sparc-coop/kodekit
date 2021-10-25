using System.Threading.Tasks;
using Sparc.Features;

namespace Kodekit.Features
{
    public record CreateKitResponse(string KitId);
    public class CreateKit : PublicFeature<string, CreateKitResponse>
    {
        public KitRepository Kits { get; }
        public CreateKit(KitRepository kits) => Kits = kits;

        public override async Task<CreateKitResponse> ExecuteAsync(string userId)
        {
            var kit = new Kit("Untitled", userId);
            await Kits.UpdateAsync((kit, null));
            
            return new(kit.Id);
        }
    }
}
