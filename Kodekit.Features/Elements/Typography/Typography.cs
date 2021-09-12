using System.Collections.Generic;

namespace Kodekit.Features.Elements
{
    public class Typography : Element
    {
        public Typography() : base("p")
        {
        }

        public Typography(string name, string fontFamily, string fontWeight, double? fontSize, double? typeScale, double? lineHeight) : base(name)
        {
            FontFamily = new(fontFamily);
            FontWeight = new(fontWeight);
            FontSize = new(fontSize);
            TypeScale = new(typeScale);
            LineHeight = new(lineHeight);

        }

        public Font FontFamily { get; set; }
        public FontWeight FontWeight { get; set; }
        public FontSize FontSize { get; set; }
        
        public TypeScale TypeScale { get; set; }
        public Size LineHeight { get; set; }

        public static Dictionary<double, string> TypeScales = new()
        {
            { 1.067, "Minor Second" },
            { 1.125, "Major Second" },
            { 1.200, "Minor Third" },
            { 1.250, "Major Third" },
            { 1.333, "Perfect Fourth" },
            { 1.414, "Augmented Fourth" },
            { 1.500, "Perfect Fifth" },
            { 1.618, "Golden Ratio" }
        };
    }
}
