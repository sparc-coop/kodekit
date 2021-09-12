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
            Color = string.Empty;
        }
        
        public Shadow(double x, double y, double blur, double spread, string color, double opacity)
        {
            X = x;
            Y = y;
            Blur = blur;
            Spread = spread;
            Color = color;
            Opacity = opacity;
        }


        public double X {  get; set; }
        public double Y {  get; set; }
        public double Blur {  get; set; }
        public double Spread {  get; set; }
        public string Color {  get; set; }
        public double Opacity {  get; set; }

        public Dictionary<string, Shadow> Expand(Shadow xLargeShadow)
        {
            return new Dictionary<string, Shadow>
            {
                { "small", this },
                { "medium", Interpolate(xLargeShadow, 0.33) },
                { "large", Interpolate(xLargeShadow, 0.66) },
                { "xlarge", xLargeShadow }
            };
        }

        public Dictionary<string, string> Serialize()
        {
            return new Dictionary<string, string>();
            
            //return new Dictionary<string, string>
            //{
            //    { "small", this },
            //    { "medium", Interpolate(xLargeShadow, 0.33) },
            //    { "large", Interpolate(xLargeShadow, 0.66) },
            //    { "xlarge", xLargeShadow }
            //};
        }

        private Shadow Interpolate(Shadow other, double percentage)
        {
            Func<double, double, double> between = (double min, double max) => min + ((max - min) * percentage);
            
            return new Shadow(between(X, other.X), 
                between(Y, other.Y),
                between(Blur, other.Blur),
                between(Spread, other.Spread),
                new Color(Color).Interpolate(new Color(other.Color), (float)percentage).HexValue,
                between(Opacity, other.Opacity));
        }
    }
}