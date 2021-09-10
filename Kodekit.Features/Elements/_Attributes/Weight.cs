using System.Collections.Generic;

namespace Kodekit.Features.Elements
{
    public record Weight
    {
        public string Value { get; set; }
        public string Placeholder { get; set; }

        public Weight()
        { }
        
        public Weight(string weight)
        {
            Value = weight;
            Placeholder = !string.IsNullOrWhiteSpace(weight) && ValidValues.ContainsKey(weight) 
                ? ValidValues[weight] 
                : string.Empty;
        }

        public static Dictionary<string, string> ValidValues = new()
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

        public override string ToString() => Value.ToString();
    }
}
