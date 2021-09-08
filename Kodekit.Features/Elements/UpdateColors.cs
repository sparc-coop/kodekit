using Sparc.Core;
using Sparc.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kodekit.Features.Elements
{
    public record UpdateColorsRequest(string KitId, string Primary, string Secondary, string Tertiary, string Darkest, string Lightest, string Error, string Warning, string Success);
    public class UpdateColors : Feature<UpdateColorsRequest, Kit>
    {
        public UpdateColors(IRepository<Kit> kits)
        {
            Kits = kits;
        }

        public IRepository<Kit> Kits { get; }

        public override async Task<Kit> ExecuteAsync(UpdateColorsRequest request)
        {
            var kit = await Kits.FindAsync(request.KitId);
            kit.SetColor(ColorTypes.Primary, request.Primary);
            kit.SetColor(ColorTypes.Secondary, request.Secondary);
            kit.SetColor(ColorTypes.Tertiary, request.Tertiary);
            kit.SetColor(ColorTypes.Error, request.Error);
            kit.SetColor(ColorTypes.Warning, request.Warning);
            kit.SetColor(ColorTypes.Success, request.Success);
            kit.SetColor(ColorTypes.Lightest, request.Lightest, request.Darkest);

            await Kits.UpdateAsync(kit);
            return kit;
        }
    }
}
