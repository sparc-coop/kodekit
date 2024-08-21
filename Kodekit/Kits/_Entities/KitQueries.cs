using Ardalis.Specification;

namespace Kodekit;

public class All : BlossomQuery<Kit>
{
    public All() => Query.OrderByDescending(x => x.DateModified);
}

public class AllFonts : BlossomQuery<GoogleFont>
{
    public AllFonts() => Query.OrderBy(x => x.Family);
}