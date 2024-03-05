using Microsoft.AspNetCore.Hosting;

namespace Kodekit;

public record CreateKitResponse(string KitId);
public class CreateKit : PublicFeature<string, CreateKitResponse>
{
    public KitRepository Kits { get; }
    public IWebHostEnvironment Env { get; }

    public CreateKit(KitRepository kits, IWebHostEnvironment env)
    {
        Kits = kits;
        Env = env;
    }

    public override async Task<CreateKitResponse> ExecuteAsync(string userId)
    {
        var kit = new Kit(Env, "Untitled", userId);
        await Kits.UpdateAsync(kit);

        return new(kit.Id);
    }
}
