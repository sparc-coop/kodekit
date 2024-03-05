namespace Kodekit;

public class GetUserKits(IRepository<Kit> kits) : Feature<List<Kit>>
{
    public IRepository<Kit> Kits { get; } = kits;

    public override async Task<List<Kit>> ExecuteAsync()
    {
        var kits = await Kits.Query
            .Where(x => x.UserId == User.Id() && x.IsDeleted != true)
            .ToListAsync();

        return kits;
    }
}
