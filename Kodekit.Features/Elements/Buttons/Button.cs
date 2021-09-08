namespace Kodekit.Features.Elements
{
    public class Button : Element
    {
        public Button() : base("button")
        {
        }

        public Button(double fontSize, int fontWeight, double verticalPadding, double horizontalPadding, double cornerRadius,
            double borderWidth, double iconWidth, double iconHeight, bool removeSecondaryBorder) : this()
        {
            FontSize = new(fontSize);
            FontWeight = new(fontWeight);
            VerticalPadding = new(verticalPadding);
            HorizontalPadding = new(horizontalPadding);
            CornerRadius = new(cornerRadius);
            BorderWidth = new(borderWidth);
            IconWidth = new(iconWidth);
            IconHeight = new(iconHeight);
            RemoveSecondaryBorder = removeSecondaryBorder;
        }

        [Css("font-size")]
        public Size FontSize { get; set; }
        [Css("font-weight")]
        public Weight FontWeight { get; set; }
        [Css("padding-top", "padding-bottom")]
        public Size VerticalPadding { get; set; }
        [Css("padding-left", "padding-right")]
        public Size HorizontalPadding { get; set; }
        [Css("border-radius")]
        public Size CornerRadius { get; set; }
        [Css("border-width")]
        public Size BorderWidth { get; set; }
        public Size IconWidth { get; set; }
        public Size IconHeight { get; set; }
        public bool RemoveSecondaryBorder { get; set; }

    }
}
