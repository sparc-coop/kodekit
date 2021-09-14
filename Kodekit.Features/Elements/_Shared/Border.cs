using System.Collections.Generic;
using System.Text;

namespace Kodekit.Features.Elements
{
    public record Border : ISerializable
    {
        public Border()
        {
        }

        public Border(double? size, double? radius = null, string? color = null)
        {
            if (size.HasValue)
                Width = new(size.Value);

            if (radius.HasValue)
                Radius = new(radius.Value);

            if (color != null)
                Color = new(color);
        }

        public Size? Width { get; set; }
        public Size? Radius { get; set; }
        public Color? Color { get; set; }

        public string? Scope => null;

        public Dictionary<string, string> Serialize()
        {
            var dict = new Dictionary<string, string>();

            if (Width != null)
                dict.Add("border-width", Width.ToString());
            if (Radius != null)
                dict.Add("border-radius", Radius.ToString());
            if (Color != null)
                dict.Add("border-color", Color.HexValue);

            return dict;
        }
    }
}
