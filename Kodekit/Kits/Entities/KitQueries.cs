using Ardalis.Specification;

namespace Kodekit;

public class All : BlossomQuery<Kit>
{
    public All() => Query.OrderByDescending(x => x.DateModified);
}