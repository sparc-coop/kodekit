using Sparc.Core;
using System;

namespace Kodekit.Features
{
    public class Kit : Root<string>
    {
        private Kit()
        {
            Id = Guid.NewGuid().ToString();
            Name = "Untitled Kit";
            DateCreated = DateTime.UtcNow;
            DateModified = DateTime.UtcNow;
        }

        public Kit(string name, string? userId = null) : this()
        {
            Name = name;
            UserId = userId;
        }

        public Kit(Kit kit) : this()
        {
            Name = kit.Name;
            UserId = kit.UserId;
        }

        internal void Update(string name, bool isAutoPublish)
        {
            Name = name;
            IsAutoPublish = isAutoPublish;
            DateModified = DateTime.UtcNow;
        }

        public string KitId => Id;
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

        // Revision links
        public string? CurrentRevisionId { get; set; }
        public string? PublishedRevisionId { get; set; }
        public string? PreviousRevisionId { get; set; }
        public string? NextRevisionId { get; set; }

        internal KitRevision AddRevision(KitRevision revision)
        {
            revision = new KitRevision(revision);
            PreviousRevisionId = CurrentRevisionId;
            CurrentRevisionId = revision.Id;

            return revision;
        }
    }
}
