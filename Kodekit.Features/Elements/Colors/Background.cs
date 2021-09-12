namespace Kodekit.Features.Elements
{
    public class Background : Color
    {
        public Background() : base()
        { }

        public Background(string hex) : base(hex)
        {
        }

        public Background(System.Drawing.Color color) : base(color)
        {
        }

        public override string ToString() => $"background: {HexValue};";
    }
}
