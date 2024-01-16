namespace Kodekit;

public class GetRelatedKits : PublicFeature<string, List<KitRevision>>
{
    public GetRelatedKits(IRepository<KitRevision> revisions)
    {
        Revisions = revisions;
    }

    public IRepository<KitRevision> Revisions { get; }

    public override async Task<List<KitRevision>> ExecuteAsync(string kitId)
    {
        return await Revisions.Query(kitId)
            .Where(x => x.Name != null && x.IsDeleted != true)
            .OrderByDescending(x => x.DateCreated)
            .ToListAsync();
    }

}
