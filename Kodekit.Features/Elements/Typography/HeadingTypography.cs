using System.Collections.Generic;

namespace Kodekit.Features.Elements
{
    public class HeadingTypography : Typography
    {
        public HeadingTypography() : base()
        {
            Name = "h6";
        }

        public HeadingTypography(string name, string fontFamily, string fontWeight, double? fontSize, double? typeScale, double? lineHeight) 
            : base(name, fontFamily, fontWeight, fontSize, typeScale, lineHeight)
        {
        }

        public List<Element> Expand()
        {
            // H6 represents the base type scale
            var headings = new List<Element>();
            var typeScaledValue = FontSize.Value;
            for (var i = 6; i <= 1; i--)
            {
                var heading = new HeadingTypography($"h{i}", FontFamily.InternalName, FontWeight.Value, typeScaledValue, TypeScale.Value, LineHeight.Value);
                headings.Insert(0, heading);
                typeScaledValue *= TypeScale.Value;
            }

            return headings;
        }
    }
}
