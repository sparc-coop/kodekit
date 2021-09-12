namespace Kodekit.Features.Elements
{
    public class Foreground : Color
    {
        public Foreground() : base()
        { }

        public Foreground(string hex) : base(hex)
        {
        }

        public Foreground(System.Drawing.Color color) : base(color)
        {
        }

        public override string ToString() => $"color: {HexValue};";
    }
}
