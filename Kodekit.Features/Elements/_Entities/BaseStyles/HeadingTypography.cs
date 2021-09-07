using System.Collections.Generic;

namespace Kodekit.Features.Elements
{
    public class HeadingTypography : Typography
    {
        public HeadingTypography() : base("h6")
        {
        }

        public List<Element> Expand()
        {
            // H6 represents the base type scale
            var headings = new List<Element>();
            var typeScaledValue = FontSize.Value;
            for (var i = 6; i <= 1; i--)
            {
                var heading = new HeadingTypography
                {
                    Name = $"h{i}",
                    FontFamily = FontFamily,
                    FontSize = FontSize with { Value = typeScaledValue },
                    LineHeight = LineHeight,
                    FontWeight = FontWeight
                };

                headings.Insert(0, heading);
                typeScaledValue *= TypeScale.Value;
            }

            return headings;
        }
    }
}
