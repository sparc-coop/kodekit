namespace Kodekit.Features.Elements
{
    public class Dropdown : Input
    {
        public Dropdown() : base()
        {
            Name = "select";
        }

        public Dropdown(double fontSize, string fontWeight, double verticalPadding, double horizontalPadding, double cornerRadius,
            double borderWidth) : base(fontSize, fontWeight, verticalPadding, horizontalPadding, cornerRadius, borderWidth)
        {
        }
    }
}
