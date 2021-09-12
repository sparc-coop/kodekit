using System.Collections.Generic;
using System.Text;

namespace Kodekit.Features.Elements
{
    public record Padding
    {
        public Padding()
        {
        }

        public Padding(double horizontalPadding, double verticalPadding)
        {
            Horizontal = new(horizontalPadding);
            Vertical = new(verticalPadding);
        }

        public Size Horizontal { get; set; }
        public Size Vertical {  get; set; }

        public override string ToString()
        {
            var str = new StringBuilder();
            if (Horizontal != null && Vertical != null)
            {
                str.AppendLine($"padding: {Vertical} {Horizontal};");
            }
            else if (Horizontal != null)
            {
                str.AppendLine($"padding-left: {Horizontal};");
                str.AppendLine($"padding-right: {Horizontal};");
            }
            else if (Vertical != null)
            {
                str.AppendLine($"padding-top: {Vertical};");
                str.AppendLine($"padding-bottom: {Vertical};");
            }

            return str.ToString();
        }
    }
}
