namespace Kodekit;

public record GoogleFontResponse(List<GoogleFont> Items);
public class GoogleFont : BlossomEntity<string>
{
    public required string Family { get; set; }
    public required string Category { get; set; }
}

public record FontWeight(string Value, string Name);
public class Font : ISerializable
{
    internal Font()
    { }

    internal Font(string? family)
    {
        if (family != null)
        {
            Family = family;
            var cleanFamily = family.Replace(" ", "+");
            FamilyUrl = $"https://fonts.googleapis.com/css2?family={cleanFamily}&display=swap";
        }
    }

    internal Font(double? size, string? weight, string? family = null, double? lineHeight = null) : this(family)
    {
        Size = size.HasValue ? new(size.Value) : null;
        Weight = weight != null && ValidWeights.Any(x => x.Value == weight) ? weight : "400";

        if (lineHeight.HasValue)
            LineHeight = new(lineHeight.Value);
    }

    public string? Family { get; set; }
    public string? FamilyUrl { get; set; }
    public string? Weight { get; set; }
    public Size? Size { get; set; }
    public Size? LineHeight { get; set; }

    public static List<FontWeight> ValidWeights =
    [
        new("100", "Thin"),
        new("200", "Extra-Light"),
        new("300", "Light"),
        new("400", "Regular"),
        new("500", "Medium"),
        new("600", "Semi-Bold"),
        new("700", "Bold"),
        new("800", "Extra-Bold"),
        new("900", "Black")
    ];

    public Dictionary<string, string> Serialize()
    {
        var dict = new Dictionary<string, string>();
        if (Family != null)
            dict.Add("font-family", $"'{Family}'");
        if (Weight != null)
            dict.Add("font-weight", Weight);
        if (Size != null)
            dict.Add("font-size", Size.ToString());
        if (LineHeight != null)
            dict.Add("line-height", LineHeight.ToString());

        return dict;
    }
}
