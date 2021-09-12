using System.Collections.Generic;
using System.Text;

namespace Kodekit.Features.Elements
{
    public record Font
    {
        public Font()
        { }
        
        public Font(string family, double? size, string? weight, double? lineHeight = null)
        {
            Family = family;

            var cleanFamily = family.Replace(" ", "+");
            FamilyUrl = $"https://fonts.googleapis.com/css?family={cleanFamily}";

            Size = size.HasValue ? new(size.Value) : null;
            Weight = weight != null && ValidWeights.ContainsKey(weight) ? weight : "400";

            if (lineHeight.HasValue)
                LineHeight = new(lineHeight.Value);
        }

        public string Family { get; set; }
        public string FamilyUrl { get; set; }
        public string? Weight { get; set; }
        public Size? Size { get; set; }
        public Size? LineHeight { get; set; }

        public override string ToString()
        {
            if (Size != null && Family != null)
            {
                var size = $"{Size}" + (LineHeight != null ? $"/{LineHeight}" : "");
                return $"font: {Weight} {size} '{Family}';";
            }

            var str = new StringBuilder();
            if (Family != null)
                str.AppendLine($"font-family: '{Family}';");

            if (Weight != null)
                str.AppendLine($"font-weight: {Weight};");

            if (Size != null)
                str.AppendLine($"font-size: {Size};");

            if (LineHeight != null)
                str.AppendLine($"line-height: {LineHeight};");

            return str.ToString();
        }

        public static Dictionary<string, string> ValidWeights = new()
        {
            { "100", "Thin" },
            { "200", "Extra-Light" },
            { "300", "Light" },
            { "400", "Regular" },
            { "500", "Medium" },
            { "600", "Semi-Bold" },
            { "700", "Bold" },
            { "800", "Extra-Bold" },
            { "900", "Black" }
        };
    }
}
