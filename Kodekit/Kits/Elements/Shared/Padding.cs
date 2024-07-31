namespace Kodekit;

public class Padding : ISerializable
{
    internal Padding()
    {
    }

    internal Padding(double? horizontal, double? vertical)
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

    internal Dictionary<string, string> Serialize(string prefix)
    {
        var dict = new Dictionary<string, string>();

        if (Vertical != null)
            dict.Add($"{prefix}-padding-vertical", Vertical.ToString());
        if (Horizontal != null)
            dict.Add($"{prefix}-padding-horizontal", Horizontal.ToString());

        return dict;
    }
}
