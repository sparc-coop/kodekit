namespace Kodekit;

public class Selector : ISerializable
{
    internal Selector()
    {
        Font = new();
    }

    internal Selector(double? fontSize, string? fontWeight, string? activeColor) : this()
    {
        Font = new(fontSize, fontWeight);

        if (!string.IsNullOrWhiteSpace(activeColor))
            ActiveColor = new(activeColor);
    }

    internal Font Font { get; set; }
    internal Color? ActiveColor { get; set; }

    public Dictionary<string, string> Serialize()
    {
        var dict = Font.Serialize();

        if (ActiveColor != null)
            foreach (var shade in ActiveColor.Serialize("active"))
                dict.Add(shade.Key, shade.Value);

        return dict;
    }
} 
