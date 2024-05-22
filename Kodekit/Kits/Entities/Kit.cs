namespace Kodekit;

public class Kit : BlossomEntity<string>
{
    private Kit()
    {
        Id = BlossomTools.FriendlyId();
        KitId = Id;
        Name = "Untitled Kit";
        DateCreated = DateTime.UtcNow;
        DateModified = DateTime.UtcNow;
        Current = new KitRevision(this);
    }

    public Kit(string name, string? userId = null) : this()
    {
        KitId = Id;
        Name = name;
        UserId = userId;
    }

    internal Kit(Kit kit) : this(kit.Name, kit.UserId)
    {
        Current = new KitRevision(kit);
    }

    public string KitId { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? UserId { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime DateModified { get; set; }
    public bool? IsAutoPublish { get; set; }
    public bool? IsDeleted { get; set; }
    public int? ThemeId { get; set; }

    // Revision links
    public string? CurrentRevisionId { get; set; }
    public string? PublishedRevisionId { get; set; }
    public string? PreviousRevisionId { get; set; }
    public string? NextRevisionId { get; set; }

    internal KitRevision Current { get; set; }

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

    internal new void Publish()
    {
        PublishedRevisionId = CurrentRevisionId;
        DateModified = DateTime.UtcNow;
    }

    internal void AddRevision()
    {
        Current = new KitRevision(this);
        PreviousRevisionId = CurrentRevisionId;
        CurrentRevisionId = Current.Id;
    }

    internal Kit Copy()
    {
        return new Kit(this);
    }

    
}
