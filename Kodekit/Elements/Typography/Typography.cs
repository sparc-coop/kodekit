using Newtonsoft.Json;

namespace Kodekit.Models.Elements;

public class Typography : ISerializable
{
    public Typography()
    {
        Font = new();
    }

    public Typography(string? fontFamily, string? fontWeight, double? fontSize, double? typeScale,
        double? lineHeight, Dictionary<string, string>? fontSizeOverride) : this()
    {
        Font = new(fontSize, fontWeight, fontFamily, lineHeight);
        TypeScale = typeScale;
        FontSizeOverrides = fontSizeOverride;
    }

    public Font Font { get; set; }
    public double? TypeScale { get; set; }
    public Dictionary<string, string>? FontSizeOverrides { get; set; }

    public static Dictionary<double, string> TypeScales = new()
    {
        { 1.067, "Minor Second" },
        { 1.125, "Major Second" },
        { 1.200, "Minor Third" },
        { 1.250, "Major Third" },
        { 1.333, "Perfect Fourth" },
        { 1.414, "Augmented Fourth" },
        { 1.500, "Perfect Fifth" },
        { 1.618, "Golden Ratio" }
    };

    public static Dictionary<string, string> FontWeights = new()
    {
        { "h1", "type-900" },
        { "h2", "type-800" },
        { "h3", "type-700" },
        { "h4", "type-600" },
        { "h5", "type-500" },
        { "h6", "type-400" },
        { ".subtitle", "type-300" }
    };

    public Dictionary<string, string> Serialize()
    {
        if (TypeScale == null)
            return Font.Serialize();

        var size = Font.Size ?? new Size(16);
        var scale = TypeScale.Value;

        var scales = new Dictionary<string, string>
            {
                { $"type-50", CheckOverride("type-50", size, scale, -4).ToString() },
                { $"type-100", CheckOverride("type-100", size, scale, -3).ToString() },
                { $"type-200", CheckOverride("type-200", size, scale, -2).ToString() },
                { $"type-300", CheckOverride("type-300", size, scale, -1).ToString() },
                { $"type-400", CheckOverride("type-400", size, scale, 0).ToString() },
                { $"type-500", CheckOverride("type-500", size, scale, 1).ToString() },
                { $"type-600", CheckOverride("type-600", size, scale, 2).ToString()},
                { $"type-700", CheckOverride("type-700", size, scale, 3).ToString() },
                { $"type-800", CheckOverride("type-800", size, scale, 4).ToString() },
                { $"type-900", CheckOverride("type-900", size, scale, 5).ToString() }
            };

        var result = scales.Concat(Font);

        // Calculate line height percentage
        if (Font.LineHeight != null)
            result["line-height"] = $"{Font.LineHeight.Value}%";

        return result;
    }

    public Size CheckOverride(string type, Size size, double scale, int multiplier)
    {
        if (FontSizeOverrides != null)
            return FontSizeOverrides.ContainsKey(type)
            ? new Size(Convert.ToDouble(FontSizeOverrides[type]))
            : size.Scale(scale, multiplier);

        return size.Scale(scale, multiplier);
    }

    public string GetOverride(string typeValue)
    {
        if (FontSizeOverrides == null)
            return "";

        if (FontSizeOverrides.TryGetValue(typeValue, out string? value))
        {
            return value;
        }
        else
        {
            return "";
        }
    }

    public void UpdateOverride(string type, string? value)
    {
        FontSizeOverrides ??= [];

        if (value == null && FontSizeOverrides.ContainsKey(type))
            FontSizeOverrides.Remove(type);
        else if (value != null)
            FontSizeOverrides[type] = value;
    }

    public string Scale(string scale)
    {
        var scales = Serialize();
        return scales.ContainsKey($"type-{scale}") == true
        ? scales[$"type-{scale}"]
        : string.Empty;
    }

    public record FontListResponse(List<GoogleFont>? Items);
    public record GoogleFont(string? Family, string? Category, FontFile? Files);
    public record FontFile(string? Regular);

    public static async Task<FontListResponse> GetGoogleFontsAsync(string apiKey)
    {
        string url = $"https://www.googleapis.com/webfonts/v1/webfonts?key={apiKey}";
        
        using HttpClient client = new();
        var fonts = await client.GetFromJsonAsync<FontListResponse>(url);
        if (fonts?.Items == null)
            throw new Exception("Failed to fetch Google Fonts");

        fonts.Items.RemoveAll(x => x.Category != "serif" && x.Category != "sans-serif");
        return fonts;
    }
}
