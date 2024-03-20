namespace Kodekit;

public class Kit : Entity<string>
{
    private Kit()
    {
        Id = Guid.NewGuid().ToString();
        KitId = Id;
        Name = "Untitled Kit";
        DateCreated = DateTime.UtcNow;
        DateModified = DateTime.UtcNow;
        Current = new KitRevision(this);
    }

    public Kit(IWebHostEnvironment env, string name, string? userId = null) : this()
    {
        Id = GenerateFriendlyId(env);
        KitId = Id;
        Name = name;
        UserId = userId;
    }

    public Kit(IWebHostEnvironment env, Kit kit) : this(env, kit.Name, kit.UserId)
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

    internal Kit Copy(IWebHostEnvironment env)
    {
        return new Kit(env, this);
    }

    string GenerateFriendlyId(IWebHostEnvironment env)
    {
        return $"{GetRandomWord(env)}-{GetRandomWord(env)}";
    }

    string GetRandomWord(IWebHostEnvironment env)
    {
        var random = new Random();
        var word = System.IO.File.ReadLines(Path.Combine(env.ContentRootPath, "_Plugins/words_alpha.txt"))
            .Skip(random.Next(370000))
            .First()
            .Trim()
            .ToLower();

        // Check against office-unsafe words
        if (System.IO.File.ReadLines(Path.Combine(env.ContentRootPath, "_Plugins/words_officesafe.txt"))
            .Any(x => x.ToLower() == word))
            return GetRandomWord(env);

        return word;
    }
}
