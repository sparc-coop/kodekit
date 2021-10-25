using Sparc.Core;
using Sparc.Features;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Kodekit.Features
{
    public class KitRepository
    {
        public KitRepository(IRepository<Kit> kits, IRepository<KitRevision> revisions)
        {
            Kits = kits;
            Revisions = revisions;
        }

        public IRepository<Kit> Kits { get; }
        public IRepository<KitRevision> Revisions { get; }

        public async Task<(Kit Kit, KitRevision Revision)> GetKitAndRevisionAsync(string kitId, string revisionId)
        {
            var kit = await GetKitAsync(kitId);

            var revision = await GetRevisionAsync(kitId, revisionId)
                ?? new KitRevision(kit);

            return (kit, revision);
        }

        internal async Task UpdateAsync(Kit kit)
        {
            await Kits.UpdateAsync(kit);
        }

        public async Task<Kit> GetKitAsync(string kitId)
        {
            var kit = await Kits.FindAsync(kitId);
            if (kit == null)
                throw new NotFoundException("Kit not found!");
            
            return kit;
        }

        public Task<KitRevision?> GetRevisionAsync(string kitId, string revisionId)
        {
            return Task.FromResult(Revisions.Query(kitId).FirstOrDefault(x => x.Id == revisionId));
        }

        public async Task<KitRevision> GetCurrentRevisionAsync(string kitId)
            => (await GetCurrentAsync(kitId)).Revision;

        public async Task<(Kit Kit, KitRevision Revision)> GetCurrentAsync(string kitId)
        {
            var kit = await GetKitAsync(kitId);
            var revision = kit.CurrentRevisionId != null
                ? await GetRevisionAsync(kitId, kit.CurrentRevisionId)
                : null;

            if (revision == null)
                throw new NotFoundException("Current revision not found!");

            return (kit, revision);
        }

        public async Task<(Kit Kit, KitRevision? Revision)> GetPublishedAsync(string kitId)
        {
            var kit = await GetKitAsync(kitId);
            var revision = kit.PublishedRevisionId != null
                ? await GetRevisionAsync(kitId, kit.PublishedRevisionId)
                : kit.CurrentRevisionId != null
                ? await GetRevisionAsync(kitId, kit.CurrentRevisionId)
                : null;

            return (kit, revision);
        }


        internal async Task UpdateAsync((Kit Kit, KitRevision? Revision) kit)
        {
            // update revision links
            var revision = kit.Kit.AddRevision(kit.Revision);

            await Kits.UpdateAsync(kit.Kit);
            await Revisions.AddAsync(revision);
        }
    }
}
