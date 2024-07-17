namespace Kodekit;

public class Dropdown : Input
{
    internal Dropdown() : base()
    {
        OverwriteInherited = false;
    }

    internal Dropdown(double? fontSize, string? fontWeight, double? verticalPadding, double? horizontalPadding, double? cornerRadius,
        double? borderWidth, bool overwriteInherited) : base(fontSize, fontWeight, verticalPadding, horizontalPadding, cornerRadius, borderWidth)
    {
        OverwriteInherited = overwriteInherited;
    }

    internal bool OverwriteInherited { get; set; }
}
