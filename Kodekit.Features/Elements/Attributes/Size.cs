using System.Collections.Generic;
using System.Linq;

namespace Kodekit.Features.Elements
{
    public record Size
    {
        public double Value { get; set; }
        public string Unit { get; set; }
        public string Placeholder { get; set; }

        public Size(string sizeString)
        {
            var digits = new string(sizeString.Where(char.IsDigit).ToArray());
            Value = double.Parse(digits);
            Unit = sizeString.Replace(digits, "").Trim().ToLower();
            Placeholder = ValidUnits.ContainsKey(Unit) ? ValidUnits[Unit] : string.Empty;
        }

        public static Dictionary<string, string> ValidUnits = new()
        {
            { "px", "pixels" },
            { "pt", "points" },
            { "em", "relative to current font-size" },
            { "rem", "relative to root font-size" },
            { "vw", "relative to viewport width" },
            { "vh", "relative to viewport height" },
            { "%", "percentage" }
        };

        public override string ToString() => $"{Value}{Unit}";
    }
}
