using System.Text;

namespace Kodekit.Features.Elements
{
    public record Border
    {
        public Border()
        {
            Width = new(0);
        }

        public Border(double size, double? radius = null, string? color = null)
        {
            Width = new(size);

            if (radius.HasValue)
                Radius = new(radius.Value);

            if (color != null)
                Color = new(color);
        }

        public Size Width { get; set; }
        public Size? Radius { get; set; }
        public Color? Color { get; set; }

        public override string ToString()
        {
            var str = new StringBuilder();
            str.AppendLine($"border-width: {Width};");
            
            if (Radius != null)
                str.AppendLine($"border-radius: {Radius};");

            if (Color != null)
                str.AppendLine($"border-color: {Color};");

            return str.ToString();
        }
    }
}
