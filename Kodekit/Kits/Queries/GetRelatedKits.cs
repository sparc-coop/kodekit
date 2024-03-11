namespace Kodekit;

public class GetRelatedKits(IRepository<KitRevision> revisions) : PublicFeature<string, List<KitRevision>>
{
    public IRepository<KitRevision> Revisions { get; } = revisions;

    public override async Task<List<KitRevision>> ExecuteAsync(string kitId)
    {
        return await Revisions.Query(kitId)
            .Where(x => x.Name != null && x.IsDeleted != true)
            .OrderByDescending(x => x.DateCreated)
            .ToListAsync();
    }

}
