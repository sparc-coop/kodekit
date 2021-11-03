using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Sparc.Features;

namespace Kodekit.Features
{
    public record CreateKitResponse(string KitId);
    public class CreateKit : PublicFeature<string, CreateKitResponse>
    {
        public KitRepository Kits { get; }
        public IWebHostEnvironment Env { get; }

        public CreateKit(KitRepository kits, IWebHostEnvironment env)
        {
            Kits = kits;
            Env = env;
        }

        public override async Task<CreateKitResponse> ExecuteAsync(string userId)
        {
            var kit = new Kit(GenerateFriendlyId(), "Untitled", userId);
            await Kits.UpdateAsync((kit, null));
            
            return new(kit.Id);
        }

        string GenerateFriendlyId()
        {
            return $"{GetRandomWord()}-{GetRandomWord()}";
        }

        private string GetRandomWord()
        {
            var random = new Random();
            var word = System.IO.File.ReadLines(System.IO.Path.Combine(Env.ContentRootPath, "_Plugins/words_alpha.txt"))
                .Skip(random.Next(370000))
                .First()
                .Trim()
                .ToLower();

            // Check against office-unsafe words
            if (System.IO.File.ReadLines(System.IO.Path.Combine(Env.ContentRootPath, "_Plugins/words_officesafe.txt"))
                .Any(x => x.ToLower() == word))
                return GetRandomWord();

            return word;
        }
    }
}
