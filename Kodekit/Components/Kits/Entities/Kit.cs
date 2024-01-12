namespace Kodekit.Features;

public class Kit : Root<string>
{
    private Kit()
    {
        Id = Guid.NewGuid().ToString();
        KitId = Id;
        Name = "Untitled Kit";
        DateCreated = DateTime.UtcNow;
        DateModified = DateTime.UtcNow;
    }

    public Kit(string id, string name, string? userId = null) : this()
    {
        Id = id;
        KitId = id;
        Name = name;
        UserId = userId;
    }

    public Kit(string id, Kit kit) : this()
    {
        Id = id;
        KitId = id;
        Name = kit.Name;
        UserId = kit.UserId;
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

    public string KitId { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? UserId { get; set; }
    public DateTime DateCreated { get; set; }

    internal void Publish()
    {
        PublishedRevisionId = CurrentRevisionId;
        DateModified = DateTime.UtcNow;
    }

    public DateTime DateModified { get; set; }
    public bool? IsAutoPublish { get; set; }
    public bool? IsDeleted { get; set; }
    public int? ThemeId { get; set; }

    // Revision links
    public string? CurrentRevisionId { get; set; }
    public string? PublishedRevisionId { get; set; }
    public string? PreviousRevisionId { get; set; }
    public string? NextRevisionId { get; set; }

    internal KitRevision AddRevision(KitRevision? previousRevision = null)
    {
        var revision =
            previousRevision == null
            ? new KitRevision(this)
            : new KitRevision(previousRevision);

        PreviousRevisionId = CurrentRevisionId;
        CurrentRevisionId = revision.Id;

        return revision;
    }
}
