namespace Kodekit.Features.Elements
{
    public class Button : Element
    {
        public Button() : base()
        {
        }

        public Button(double? fontSize, string? fontWeight, double? verticalPadding, double? horizontalPadding, double? cornerRadius,
            double? borderWidth, double? iconWidth, double? iconHeight, bool removeSecondaryBorder) : this()
        {
            Font = new(Variables.Serif, fontSize, fontWeight);
            Padding = new(horizontalPadding ?? 16, verticalPadding ?? 8);
            Border = new(borderWidth ?? 0, cornerRadius ?? 4);
            IconWidth = new(iconWidth ?? 16);
            IconHeight = new(iconHeight ?? 16);
            RemoveSecondaryBorder = removeSecondaryBorder;
        }

        public Font Font { get; set; }
        public Padding Padding { get; set; }
        public Border Border { get; set; }
        public Size IconWidth { get; set; }
        public Size IconHeight { get; set; }
        public bool RemoveSecondaryBorder { get; set; }

        public override string ToString()
        {
            return
                ToCss("button",
                Border,
                Padding,
                Font,
                new Background("$primary-500"),
                new Foreground(System.Drawing.Color.White),
                new Attribute("cursor", "pointer"))

            + ToCss("button:hover", new Background("$primary-300"))

            + ToCss("button.secondary",
                new Background(System.Drawing.Color.White),
                Border with { Width = new(RemoveSecondaryBorder ? 0 : 1), Color = new("$secondary-500") },
                new Foreground("$secondary-500"))

            + ToCss("button.secondary:hover",
                Border with { Color = new("$secondary-300") },
                new Foreground("$secondary-300"))

            + ToCss("button > :first-child", new Attribute("margin-right", "14px"));
        }
    }
}
