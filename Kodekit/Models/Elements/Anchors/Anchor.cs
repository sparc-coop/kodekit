using Kodekit.Models.Elements;

namespace Kodekit.Models.Elements;

public class Anchor : ISerializable
{
    public Anchor() : base()
    {
        Font = new();
    }

    public Anchor(double? fontSize, string? fontWeight, string? defaultColor, string? hoverColor, string? visitedColor, string? activeColor) : this()
    {
        Font = new(fontSize, fontWeight);

        if (defaultColor != null) DefaultColor = new(defaultColor);
        if (hoverColor != null) HoverColor = new(hoverColor);
        if (visitedColor != null) VisitedColor = new(visitedColor);
        if (activeColor != null) ActiveColor = new(activeColor);
    }

    public Font Font { get; set; }
    public Color? DefaultColor { get; set; }
    public Color? HoverColor { get; set; }
    public Color? VisitedColor { get; set; }
    public Color? ActiveColor { get; set; }


    public Dictionary<string, string> Serialize()
    {
        var dict = Font.Serialize();

        if (DefaultColor != null)
            dict = dict.Concat(DefaultColor.Serialize("default")).ToDictionary(x => x.Key, x => x.Value);

        if (HoverColor != null)
            dict = dict.Concat(HoverColor.Serialize("hover")).ToDictionary(x => x.Key, x => x.Value);

        if (VisitedColor != null)
            dict = dict.Concat(VisitedColor.Serialize("visited")).ToDictionary(x => x.Key, x => x.Value);

        if (ActiveColor != null)
            dict = dict.Concat(ActiveColor.Serialize("active")).ToDictionary(x => x.Key, x => x.Value);

        return dict;
    }
}
