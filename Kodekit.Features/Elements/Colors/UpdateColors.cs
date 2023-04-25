namespace Kodekit.Features.Elements;

public record ColorsModel(string KitId, string? Primary, string? Secondary, string? Tertiary, string? Darkest, string? Lightest, string? Error, string? Warning, string? Success, Dictionary<string, string>? PrimaryColorOverrides, Dictionary<string, string>? SecondaryColorOverrides, Dictionary<string, string>? TertiaryColorOverrides);
public class UpdateColors : PublicFeature<ColorsModel, Kit>
{
    public UpdateColors(KitRepository kits)
    {
        Kits = kits;
    }

    public KitRepository Kits { get; }

    public override async Task<Kit> ExecuteAsync(ColorsModel request)
    {
        var kit = await Kits.GetCurrentAsync(request.KitId);
        var revision = kit.Revision;

        // checks if any of the Color schemes that have automatically generated gradient
        // (e.g., Primary, Secondary, Tertiary, Grayscale TBD)
        // have overrides and if so, calls UpdateColor() to update that color
        if (request.PrimaryColorOverrides != null)
        {
            foreach (var color in request.PrimaryColorOverrides)
            {
                if (color.Key.Contains("Primary"))
                {
                    revision.UpdateColor(ColorTypes.Primary, color.Value, color.Key.Replace("Primary", ""));
                }
            }
        }
        if (request.SecondaryColorOverrides != null)
        {
            foreach (var color in request.SecondaryColorOverrides)
            {
                revision.UpdateColor(ColorTypes.Secondary, color.Value, color.Key.Replace("Secondary", ""));
            }
        }
        if (request.TertiaryColorOverrides != null)
        {
            foreach (var color in request.TertiaryColorOverrides)
            {
                if (color.Key.Contains("Tertiary"))
                {
                    revision.UpdateColor(ColorTypes.Tertiary, color.Value, color.Key.Replace("Tertiary", ""));
                }
            }
        }

        revision.UpdateColor(ColorTypes.Primary, request.Primary, null);
        revision.UpdateColor(ColorTypes.Secondary, request.Secondary, null);
        revision.UpdateColor(ColorTypes.Tertiary, request.Tertiary, null);
        revision.UpdateColor(ColorTypes.Error, request.Error, null);
        revision.UpdateColor(ColorTypes.Warning, request.Warning, null);
        revision.UpdateColor(ColorTypes.Success, request.Success, null);
        revision.UpdateColor(ColorTypes.Lightest, request.Lightest, null);
        revision.UpdateColor(ColorTypes.Darkest, request.Darkest, null);
        
        await Kits.UpdateAsync(kit);
        return kit.Kit;
    }
}
