using System.Collections.Generic;

namespace Kodekit.Features.Elements
{
    public class Typography : ISerializable
    {
        public Typography()
        {
            Font = new();
        }

        public Typography(string? fontFamily, string? fontWeight, double? fontSize, double? typeScale, double? lineHeight) : this()
        {
            Font = new(fontSize, fontWeight, fontFamily, lineHeight);
            TypeScale = typeScale;
        }

        public Font Font { get; set; }
        public double? TypeScale { get; set; }

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

        public Dictionary<string, string> Serialize()
        {
            if (TypeScale == null)
                return Font.Serialize();

            var size = Font.Size ?? new Size(16);
            var scale = TypeScale.Value;

            var scales = new Dictionary<string, string>
            {
                { $"type-50", size.Scale(scale, -4).ToString() },
                { $"type-100", size.Scale(scale, -3).ToString() },
                { $"type-200", size.Scale(scale, -2).ToString() },
                { $"type-300", size.Scale(scale, -1).ToString() },
                { $"type-400", size.ToString() },
                { $"type-500", size.Scale(scale, 1).ToString() },
                { $"type-600", size.Scale(scale, 2).ToString() },
                { $"type-700", size.Scale(scale, 3).ToString() },
                { $"type-800", size.Scale(scale, 4).ToString() },
                { $"type-900", size.Scale(scale, 5).ToString() }
            };

            var result = scales.Concat(Font);

            // Calculate line height percentage
            if (Font.LineHeight != null)
                result["line-height"] = $"{Font.LineHeight.Value / size.Value * 100}%";

            return result;
        }
    }
}
