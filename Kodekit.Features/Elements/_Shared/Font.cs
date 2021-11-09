namespace Kodekit.Features.Elements;

public record Font : ISerializable
{
    public Font()
    { }

    public Font(double? size, string? weight, string? family = null, double? lineHeight = null)
    {
        if (family != null)
        {
            Family = family;
            var cleanFamily = family.Replace(" ", "+");
            FamilyUrl = $"https://fonts.googleapis.com/css2?family={cleanFamily}&display=swap";
        }

        Size = size.HasValue ? new(size.Value) : null;
        Weight = weight != null && ValidWeights.ContainsKey(weight) ? weight : "400";

        if (lineHeight.HasValue)
            LineHeight = new(lineHeight.Value);
    }

    public string? Family { get; set; }
    public string? FamilyUrl { get; set; }
    public string? Weight { get; set; }
    public Size? Size { get; set; }
    public Size? LineHeight { get; set; }

    public static Dictionary<string, string> ValidWeights = new()
    {
        { "100", "Thin" },
        { "200", "Extra-Light" },
        { "300", "Light" },
        { "400", "Regular" },
        { "500", "Medium" },
        { "600", "Semi-Bold" },
        { "700", "Bold" },
        { "800", "Extra-Bold" },
        { "900", "Black" }
    };

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
