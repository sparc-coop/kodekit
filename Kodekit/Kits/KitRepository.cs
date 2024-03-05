using Microsoft.AspNetCore.Hosting;
using System.Security.Claims;

namespace Kodekit;

public class KitRepository(IRepository<Kit> kits, IRepository<KitRevision> revisions, IWebHostEnvironment env, ClaimsPrincipal user)
{
    public IRepository<Kit> Kits { get; } = kits;
    public IRepository<KitRevision> Revisions { get; } = revisions;
    public IWebHostEnvironment Env { get; } = env;
    public ClaimsPrincipal User { get; } = user;

    // CREATE
    public record CreateKitResponse(string KitId);

    public async Task<CreateKitResponse> CreateKitAsync()
    {
        var kit = new Kit(Env, "Untitled", User.Id());
        await UpdateAsync(kit);

        return new(kit.Id);
    }

    public async Task<Kit> GetAsync(string kitId)
    {
        var kit = await Kits.FindAsync(kitId)
            ?? throw new Exception("Kit not found!");

        kit.Current = kit.CurrentRevisionId != null
            ? await GetRevisionAsync(kitId, kit.CurrentRevisionId)
            : null;

        return kit;
    }

    public async Task<CreateKitResponse> CopyKitAsync(string kitId)
    {
        var kit = await GetAsync(kitId);

        var newKit = new Kit(Env, kit)
        {
            Current = new KitRevision(kit)
        };

        await UpdateAsync(newKit);

        return new(newKit.Id);
    }

    public async Task<bool> PublishKitAsync(Kit kit)
    {
        try
        {
            //Verify kit can be published
            if (kit.PublishedRevisionId != null && string.IsNullOrEmpty(kit.UserId))
            {
                //return sorry cannot publish a new version unless you sign in.
                return false;
            }
            else
            {
                kit.Publish();
                await UpdateAsync(kit);
                return true;
            }
        }
        catch
        {
            return false;
        }
    }

    public Task<KitRevision?> GetRevisionAsync(string kitId, string revisionId)
    {
        var revision = Revisions.Query(kitId).FirstOrDefault(x => x.Id == revisionId);
        return Task.FromResult(revision);
    }
    
    public async Task<Kit> GetPublishedAsync(string kitId)
    {
        var kit = await GetAsync(kitId);
        kit.Current = kit.PublishedRevisionId != null
            ? await GetRevisionAsync(kitId, kit.PublishedRevisionId)
            : kit.CurrentRevisionId != null
            ? await GetRevisionAsync(kitId, kit.CurrentRevisionId)
            : null;

        return kit;
    }
    public async Task<List<KitRevision>> GetRelatedKitsAsync(string kitId)
    {
        return await Revisions.Query(kitId)
            .Where(x => x.Name != null && x.IsDeleted != true)
            .OrderByDescending(x => x.DateCreated)
            .ToListAsync();
    }

    public async Task<List<Kit>> GetUserKitsAsync(string userId)
    {
        return await Kits.Query
            .Where(x => x.UserId == userId && x.IsDeleted != true)
            .ToListAsync();
    }

    // UPDATE
    internal async Task UpdateAsync(Kit kit)
    {
        kit.AddRevision();
        await Kits.UpdateAsync(kit);
        await Revisions.AddAsync(kit.Current!);
    }

    internal async Task UpdateAsync(string KitId, string Name, string? UserId = null, bool IsAutoPublish = false)
    {
        var kit = await GetAsync(KitId);

        if (string.IsNullOrWhiteSpace(kit.UserId) && User?.Id() != null)
            kit.SetUser(User.Id());

        kit.Update(Name, IsAutoPublish);
        await Kits.UpdateAsync(kit);
    }

    public async Task<bool> SetKitThemeAsync(string kitId)
    {
        try
        {
            var kit = await GetAsync(kitId);
            //if theme not already set
            if (kit.ThemeId != null)
                return false;

            kit.ThemeId = 1;
            await UpdateAsync(kit);
            return true;

        }
        catch
        {
            return false;
        }
    }

    // DELETE
    public async Task<bool> DeleteKitAsync(string kitId)
    {
        var kit = await GetAsync(kitId);
        if (kit.UserId != User.Id())
            throw new Exception("You do not own this kit!");

        try
        {
            kit.IsDeleted = true;
            await Kits.UpdateAsync(kit);
            return true;
        }
        catch
        {
            return false;
        }
    }
}
