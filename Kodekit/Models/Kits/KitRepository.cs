using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using System.Security.Claims;

namespace Kodekit;

public class KitRepository
{
    public KitRepository(IRepository<Kit> kits, IRepository<KitRevision> revisions)
    {
        Kits = kits;
        Revisions = revisions;
    }

    public IRepository<Kit> Kits { get; }
    public IRepository<KitRevision> Revisions { get; }
    public IWebHostEnvironment Env { get; }
    public ClaimsPrincipal User { get; }

    // CREATE
    public record CreateKitResponse(string KitId);

    public async Task<CreateKitResponse> CreateKitAsync(string userId)
    {
        var kit = new Kit(GenerateFriendlyId(), "Untitled", userId);
        await UpdateAsync((kit, null));

        return new(kit.Id);
    }

    string GenerateFriendlyId()
    {
        return $"{GetRandomWord()}-{GetRandomWord()}";
    }

    private string GetRandomWord()
    {
        var random = new Random();
        var word = System.IO.File.ReadLines(System.IO.Path.Combine(Env.ContentRootPath, "_Plugins/words_alpha.txt"))
            .Skip(random.Next(370000))
            .First()
            .Trim()
            .ToLower();

        // Check against office-unsafe words
        if (System.IO.File.ReadLines(System.IO.Path.Combine(Env.ContentRootPath, "_Plugins/words_officesafe.txt"))
            .Any(x => x.ToLower() == word))
            return GetRandomWord();

        return word;
    }

    public async Task<CreateKitResponse> CopyKitAsync(string kitId)
    {
        var kit = await GetCurrentAsync(kitId);

        var newKit = new Kit(Guid.NewGuid().ToString(), kit.Kit);
        var newRevision = new KitRevision(kit.Revision);

        await UpdateAsync((newKit, newRevision));

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
                await UpdateAsync((kit, null));
                return true;
            }
        }
        catch
        {
            return false;
        }
    }

    // GET
    public async Task<(Kit Kit, KitRevision Revision)> GetKitAndRevisionAsync(string kitId, string revisionId)
    {
        var kit = await GetKitAsync(kitId);

        var revision = await GetRevisionAsync(kitId, revisionId)
            ?? new KitRevision(kit);

        return (kit, revision);
    }

    public async Task<Kit> GetKitAsync(string kitId)
    {
        var kit = await Kits.FindAsync(kitId);
        if (kit == null)
            throw new NotFoundException("Kit not found!");

        return kit;
    }

    public Task<KitRevision?> GetRevisionAsync(string kitId, string revisionId)
    {
        return Task.FromResult(Revisions.Query(kitId).AsNoTracking().FirstOrDefault(x => x.Id == revisionId));
    }

    public async Task<KitRevision> GetCurrentRevisionAsync(string kitId)
        => (await GetCurrentAsync(kitId)).Revision;

    public async Task<(Kit Kit, KitRevision Revision)> GetCurrentAsync(string kitId)
    {
        var kit = await GetKitAsync(kitId);
        var revision = kit.CurrentRevisionId != null
            ? await GetRevisionAsync(kitId, kit.CurrentRevisionId)
            : null;

        if (revision == null)
            throw new NotFoundException("Current revision not found!");

        return (kit, revision);
    }

    public async Task<(Kit Kit, KitRevision? Revision)> GetPublishedAsync(string kitId)
    {
        var kit = await GetKitAsync(kitId);
        var revision = kit.PublishedRevisionId != null
            ? await GetRevisionAsync(kitId, kit.PublishedRevisionId)
            : kit.CurrentRevisionId != null
            ? await GetRevisionAsync(kitId, kit.CurrentRevisionId)
            : null;

        return (kit, revision);
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
    internal async Task UpdateAsync((Kit Kit, KitRevision? Revision) kit)
    {
        // update revision links
        var revision = kit.Kit.AddRevision(kit.Revision);

        await Kits.UpdateAsync(kit.Kit);
        await Revisions.AddAsync(revision);
    }

    internal async Task UpdateAsync(UpdateKitRequest request)
    {
        var kit = await GetKitAsync(request.KitId);

        if (string.IsNullOrWhiteSpace(kit.UserId) && User?.Id() != null)
            kit.SetUser(User.Id());

        kit.Update(request.Name, request.IsAutoPublish);
        await Kits.UpdateAsync(kit);
    }

    public async Task<bool> SetKitThemeAsync(string kitId)
    {
        try
        {
            var kit = await GetCurrentAsync(kitId);
            //if theme not already set
            if (kit.Kit.ThemeId != null)
                return false;

            kit.Kit.ThemeId = 1;
            kit.Revision = new KitRevision(true, kitId, kit.Kit.CurrentRevisionId);

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
        var kit = await GetKitAsync(kitId);
        if (kit.UserId != User.Id())
            throw new NotAuthorizedException("You do not own this kit!");

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
