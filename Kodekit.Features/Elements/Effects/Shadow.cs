namespace Kodekit.Features.Elements;

public class Shadow : ISerializable
{
    public Shadow()
    {
        X = 0;
        Y = 0;
        Blur = 0;
        Spread = 0;
        Opacity = 0;
        HexColor = string.Empty;
    }

    public Shadow(double x, double y, double blur, double spread, string color, double opacity)
    {
        X = x;
        Y = y;
        Blur = blur;
        Spread = spread;
        HexColor = color;
        Opacity = opacity;
    }


    public double X { get; set; }
    public double Y { get; set; }
    public double Blur { get; set; }
    public double Spread { get; set; }
    protected Color Color => new(HexColor);
    public string HexColor { get; set; }
    public double Opacity { get; set; }

    public Dictionary<string, string> Expand(Shadow xLargeShadow)
    {
        return new Dictionary<string, string>
            {
                { "shadow-50", ToString() },
                { "shadow-100", Interpolate(xLargeShadow, 0.1).ToString() },
                { "shadow-200", Interpolate(xLargeShadow, 0.2).ToString() },
                { "shadow-300", Interpolate(xLargeShadow, 0.3).ToString() },
                { "shadow-400", Interpolate(xLargeShadow, 0.4).ToString() },
                { "shadow-500", Interpolate(xLargeShadow, 0.5).ToString() },
                { "shadow-600", Interpolate(xLargeShadow, 0.6).ToString() },
                { "shadow-700", Interpolate(xLargeShadow, 0.7).ToString() },
                { "shadow-800", Interpolate(xLargeShadow, 0.8).ToString() },
                { "shadow-900", Interpolate(xLargeShadow, 0.9).ToString() },
                { "shadow-1000", xLargeShadow.ToString() }
            };
    }

    public Dictionary<string, string> Serialize()
    {
        return new Dictionary<string, string>
            {
                { "shadow", ToString() }
            };
    }

    public override string ToString()
    {
        return $"{X}px {Y}px {Blur}px {Spread}px {Color.RgbaValue(0.04)}, " +
            $"{X * 0.25}px {Y * 0.25}px {Blur * 0.25}px {Spread * 0.25}px {Color.RgbaValue(0.04)}, " +
            $"0px 0px {Blur * 0.1}px {Spread * 0.1} {Color.RgbaValue(0.04)}";
    }

    private Shadow Interpolate(Shadow other, double percentage)
    {
        double between(double min, double max) => min + ((max - min) * percentage);

        return new Shadow(between(X, other.X),
            between(Y, other.Y),
            between(Blur, other.Blur),
            between(Spread, other.Spread),
            Color.Interpolate(other.Color, (float)percentage).HexValue,
            between(Opacity, other.Opacity));
    }
}
