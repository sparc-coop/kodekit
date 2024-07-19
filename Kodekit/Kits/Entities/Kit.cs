namespace Kodekit;

public class Kit : BlossomEntity<string>
{
    private Kit()
    {
        Id = Guid.NewGuid().ToString();
        KitId = Id;
        Name = "Untitled Kit";
        DateCreated = DateTime.UtcNow;
        DateModified = DateTime.UtcNow;
        Current = new(this);
    }

    public Kit(string name, string? userId = null) : this()
    {
        Id = BlossomTools.FriendlyId();
        KitId = Id;
        Name = name;
        UserId = userId;
    }

    internal Kit(Kit copyFromKit) : this(copyFromKit.Name)
    {
        UserId = copyFromKit.UserId;
    }

    public string KitId { get; private set; }
    public string Name { get; private set; }
    public string? Description { get; private set; }
    public string? UserId { get; private set; }
    public DateTime DateCreated { get; private set; }
    public DateTime DateModified { get; private set; }
    public bool? IsAutoPublish { get; private set; }
    public bool? IsDeleted { get; private set; }
    public int? ThemeId { get; private set; }

    // Revision links
    internal string? CurrentRevisionId { get; private set; }
    internal string? PublishedRevisionId { get; private set; }
    internal string? PreviousRevisionId { get; private set; }
    internal string? NextRevisionId { get; private set; }
    public KitRevision Current { get; private set; }

    internal void PublishCurrent()
    {
        PublishedRevisionId = CurrentRevisionId;
        DateModified = DateTime.UtcNow;
    }

    internal void SetUser(string id)
    {
        UserId = id;
        DateModified = DateTime.UtcNow;
    }

    internal void Update(string name, bool isAutoPublish)
    {
        Name = name;
        IsAutoPublish = isAutoPublish;
        DateModified = DateTime.UtcNow;
    }

    internal KitRevision AddRevision(KitRevision? previousRevision = null)
    {
        var revision =
            previousRevision == null
            ? new KitRevision(this)
            : new KitRevision(previousRevision);

        PreviousRevisionId = CurrentRevisionId;
        CurrentRevisionId = revision.Id;
        Current = revision;

        return revision;
    }
}
