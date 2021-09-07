using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodekit.Features.Elements
{
    public class Color
    {
        public Color(string hex)
        {
            HexValue = hex.Replace("#", "").ToUpper();
            if (HexValue.Length != 6)
                throw new ArgumentOutOfRangeException("hex", "Hex value is not valid");
        }

        public string HexValue { get; set; }

        public override string ToString() => $"#{HexValue}";
    }

    public enum Colors
    {
        Primary,
        Secondary,
        Tertiary,
        Darkest,
        Lightest,

    }
}
