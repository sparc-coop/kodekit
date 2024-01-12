namespace Kodekit.Features.Components.Elements;

public class Input : ISerializable
{
    public Input() : base()
    {
        Font = new();
        Padding = new();
        Border = new();
    }

    public Input(double? fontSize, string? fontWeight, double? verticalPadding, double? horizontalPadding, double? cornerRadius,
        double? borderWidth) : this()
    {
        Font = new(fontSize, fontWeight);
        Padding = new(horizontalPadding, verticalPadding);
        Border = new(borderWidth, cornerRadius);
    }

    public Font Font { get; set; }
    public Padding Padding { get; set; }
    public Border Border { get; set; }

    public Dictionary<string, string> Serialize()
    {
        return Font.Concat(Padding).Concat(Border);
    }
}
