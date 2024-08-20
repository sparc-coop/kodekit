namespace Kodekit;

public record TypeScale(double Value, string Name);
public class Typography : ISerializable
{
    internal Typography()
    {
        Font = new();
    }

    internal Typography(string? fontFamily, string? fontWeight, double? fontSize, double? typeScale, 
        double? lineHeight, Dictionary<string, string>? fontSizeOverride) : this()
    {
        Font = new(fontSize, fontWeight, fontFamily, lineHeight);
        TypeScale = typeScale;
        FontSizeOverrides = fontSizeOverride;
    }

    public Font Font { get; set; }
    public double? TypeScale { get; set; }
    public Dictionary<string, string>? FontSizeOverrides { get; set; }

    internal static List<TypeScale> TypeScales = 
    [
        new(1.067, "Minor Second"),
        new(1.125, "Major Second"),
        new(1.200, "Minor Third"),
        new(1.250, "Major Third"),
        new(1.333, "Perfect Fourth"),
        new(1.414, "Augmented Fourth"),
        new(1.500, "Perfect Fifth"),
        new(1.618, "Golden Ratio")
    ];
    

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

    internal Size CheckOverride(string type, Size size, double scale, int multiplier)
    {
        if(FontSizeOverrides != null)
            return FontSizeOverrides.ContainsKey(type)
            ? new Size(Convert.ToDouble(FontSizeOverrides[type]))
            : size.Scale(scale, multiplier);

        return size.Scale(scale, multiplier);
    }
}
