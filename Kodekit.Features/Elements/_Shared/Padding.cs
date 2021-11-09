namespace Kodekit.Features.Elements;

public record Padding : ISerializable
{
    public Padding()
    {
    }

    public Padding(double? horizontal, double? vertical)
    {
        if (horizontal.HasValue)
            Horizontal = new(horizontal.Value);

        if (vertical.HasValue)
            Vertical = new(vertical.Value);
    }

    public Size? Vertical { get; set; }
    public Size? Horizontal { get; set; }

    public Dictionary<string, string> Serialize()
    {
        var dict = new Dictionary<string, string>();

        if (Vertical != null)
            dict.Add("padding-vertical", Vertical.ToString());
        if (Horizontal != null)
            dict.Add("padding-horizontal", Horizontal.ToString());

        return dict;
    }

    public Dictionary<string, string> Serialize(string prefix)
    {
        var dict = new Dictionary<string, string>();

        if (Vertical != null)
            dict.Add($"{prefix}-padding-vertical", Vertical.ToString());
        if (Horizontal != null)
            dict.Add($"{prefix}-padding-horizontal", Horizontal.ToString());

        return dict;
    }
}
