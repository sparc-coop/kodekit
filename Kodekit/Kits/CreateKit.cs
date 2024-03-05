using Microsoft.AspNetCore.Hosting;

namespace Kodekit;

public record CreateKitResponse(string KitId);
public class CreateKit(KitRepository kits, IWebHostEnvironment env) : PublicFeature<string, CreateKitResponse>
{
    public KitRepository Kits { get; } = kits;
    public IWebHostEnvironment Env { get; } = env;

    public override async Task<CreateKitResponse> ExecuteAsync(string userId)
    {
        var kit = new Kit(Env, "Untitled", userId);
        await Kits.UpdateAsync(kit);

        return new(kit.Id);
    }
}
