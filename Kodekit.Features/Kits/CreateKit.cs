using System.Threading.Tasks;
using Sparc.Core;
using Sparc.Features;

namespace Kodekit.Features
{
    public record CreateKitResponse(string KitId);
    public class CreateKit : PublicFeature<string, CreateKitResponse>
    {
        public IRepository<Kit> Kits { get; }
        public CreateKit(IRepository<Kit> kit) => Kits = kit;

        public override async Task<CreateKitResponse> ExecuteAsync(string userId)
        {
            var kit = new Kit("Untitled", userId);
            await Kits.AddAsync(kit);
            
            return new(kit.Id);
        }
    }
}
