namespace Kodekit;

public class GetUserKits : Feature<List<Kit>>
{
    public GetUserKits(IRepository<Kit> kits)
    {
        Kits = kits;
    }

    public IRepository<Kit> Kits { get; }

    public override async Task<List<Kit>> ExecuteAsync()
    {
        var kits = await Kits.Query
            .Where(x => x.UserId == User.Id() && x.IsDeleted != true)
            .ToListAsync();

        return kits;
    }
}
