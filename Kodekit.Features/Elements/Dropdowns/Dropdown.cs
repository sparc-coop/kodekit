namespace Kodekit.Features.Elements
{
    public class Dropdown : Element
    {
        public Dropdown() : base("select")
        {
        }

        public Dropdown(double fontSize, string fontWeight, double verticalPadding, double horizontalPadding, double cornerRadius,
            double borderWidth) : this()
        {
            FontSize = new(fontSize);
            FontWeight = new(fontWeight);
            VerticalPadding = new(verticalPadding);
            HorizontalPadding = new(horizontalPadding);
            CornerRadius = new(cornerRadius);
            BorderWidth = new(borderWidth);
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
    }
}
