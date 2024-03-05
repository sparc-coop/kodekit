namespace Kodekit.Models.Elements;

public class Button : ISerializable
{
    public Button() : base()
    {
        Font = new();
        Padding = new();
        Border = new();
    }

    public Button(double? fontSize, string? fontWeight, double? verticalPadding, double? horizontalPadding, double? cornerRadius,
        double? borderWidth, double? iconWidth, double? iconHeight, bool removeSecondaryBorder) : this()
    {
        Font = new(fontSize, fontWeight);
        Padding = new(horizontalPadding, verticalPadding);
        Border = new(borderWidth, cornerRadius);

        if (iconWidth.HasValue)
            IconWidth = new(iconWidth.Value);

        if (iconHeight.HasValue)
            IconHeight = new(iconHeight.Value);

        RemoveSecondaryBorder = removeSecondaryBorder;
    }

    public Font Font { get; set; }
    public Padding Padding { get; set; }
    public Border Border { get; set; }
    public Size? IconWidth { get; set; }
    public Size? IconHeight { get; set; }
    public bool RemoveSecondaryBorder { get; set; }

    public Dictionary<string, string> Serialize()
    {
        var dict = Font.Concat(Padding).Concat(Border);

        if (IconWidth != null)
            dict.Add("icon-width", IconWidth.ToString());

        if (IconHeight != null)
            dict.Add("icon-height", IconHeight.ToString());

        if (RemoveSecondaryBorder)
        {
            dict.Add("secondary-border-width", Border?.Width?.ToString() ?? "0");
            dict.Add("secondary-button-background", "transparent");
        }
        else
        {
            dict.Add("secondary-button-color", "white");
        }

        return dict;
    }
}
