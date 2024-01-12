namespace Kodekit.Features.Components.Elements;

public class Dropdown : Input
{
    public Dropdown() : base()
    {
        OverwriteInherited = false;
    }

    public Dropdown(double? fontSize, string? fontWeight, double? verticalPadding, double? horizontalPadding, double? cornerRadius,
        double? borderWidth, bool overwriteInherited) : base(fontSize, fontWeight, verticalPadding, horizontalPadding, cornerRadius, borderWidth)
    {
        OverwriteInherited = overwriteInherited;
    }

    public bool OverwriteInherited { get; set; }
}
