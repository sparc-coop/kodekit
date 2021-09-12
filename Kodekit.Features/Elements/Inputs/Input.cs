namespace Kodekit.Features.Elements
{
    public class Input : Element
    {
        public Input() : base()
        {
        }

        public Input(double fontSize, string fontWeight, double verticalPadding, double horizontalPadding, double cornerRadius,
            double borderWidth) : this()
        {
            Font = new(Variables.Serif, fontSize, fontWeight);
            Padding = new(horizontalPadding, verticalPadding);
            Border = new(borderWidth, cornerRadius);
        }

        public Font Font { get; set; }
        public Padding Padding { get; set; }
        public Border Border { get; set; }
    }
}
