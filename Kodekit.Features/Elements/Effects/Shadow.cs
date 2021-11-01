using System;
using System.Collections.Generic;

namespace Kodekit.Features.Elements
{
    public class Shadow : ISerializable
    {
        public Shadow()
        {
            X = 0;
            Y = 0;
            Blur = 0;
            Spread = 0;
            Opacity = 0;
            Color = new();
        }
        
        public Shadow(double x, double y, double blur, double spread, string color, double opacity)
        {
            X = x;
            Y = y;
            Blur = blur;
            Spread = spread;
            Color = new(color);
            Opacity = opacity;
        }


        public double X {  get; set; }
        public double Y {  get; set; }
        public double Blur {  get; set; }
        public double Spread {  get; set; }
        public Color Color {  get; set; }
        public double Opacity {  get; set; }

        public Dictionary<string, Shadow> Expand(Shadow xLargeShadow)
        {
            return new Dictionary<string, Shadow>
            {
                { "shadow-50", this },
                { "shadow-100", Interpolate(xLargeShadow, 0.1) },
                { "shadow-200", Interpolate(xLargeShadow, 0.2) },
                { "shadow-300", Interpolate(xLargeShadow, 0.3) },
                { "shadow-400", Interpolate(xLargeShadow, 0.4) },
                { "shadow-500", Interpolate(xLargeShadow, 0.5) },
                { "shadow-600", Interpolate(xLargeShadow, 0.6) },
                { "shadow-700", Interpolate(xLargeShadow, 0.7) },
                { "shadow-800", Interpolate(xLargeShadow, 0.8) },
                { "shadow-900", Interpolate(xLargeShadow, 0.9) },
                { "shadow-1000", xLargeShadow }
            };
        }

        public override string ToString()
        {
            return $"{X}px {Y}px {Blur}px {Spread}px {Color.RgbaValue(0.04)}, " +
                $"{X * 0.25}px {Y * 0.25}px {Blur * 0.25}px {Spread * 0.25}px {Color.RgbaValue(0.04)}, " +
                $"0px 0px {Blur * 0.1}px {Spread * 0.1} {Color.RgbaValue(0.04)};";
        }

        private Shadow Interpolate(Shadow other, double percentage)
        {
            Func<double, double, double> between = (double min, double max) => min + ((max - min) * percentage);
            
            return new Shadow(between(X, other.X), 
                between(Y, other.Y),
                between(Blur, other.Blur),
                between(Spread, other.Spread),
                Color.Interpolate(other.Color, (float)percentage).HexValue,
                between(Opacity, other.Opacity));
        }
    }
}