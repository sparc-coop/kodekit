using System.Collections.Generic;

namespace Kodekit.Features.Elements
{
    public record Scale
    {
        public double Value { get; set; }

        public static Dictionary<double, string> ValidValues = new()
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

        public override string ToString() => Value.ToString();
    }
}