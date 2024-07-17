namespace Kodekit;

internal record Border : ISerializable
{
    internal Border()
    {
    }

    internal Border(double? size, double? radius = null, string? color = null)
    {
        if (size.HasValue)
            Width = new(size.Value);

        if (radius.HasValue)
            Radius = new(radius.Value);

        if (color != null)
            Color = new(color);
    }

    internal Size? Width { get; set; }
    internal Size? Radius { get; set; }
    internal Color? Color { get; set; }

    internal string? Scope => null;

    public Dictionary<string, string> Serialize()
    {
        var dict = new Dictionary<string, string>();

        if (Width != null)
            dict.Add("border-width", Width.ToString());
        if (Radius != null)
            dict.Add("border-radius", Radius.ToString());
        if (Color != null)
            dict.Add("border-color", Color.HexValue);

        return dict;
    }
}
