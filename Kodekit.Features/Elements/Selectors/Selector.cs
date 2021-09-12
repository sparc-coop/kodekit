using System.Collections.Generic;

namespace Kodekit.Features.Elements
{
    public class Selector : ISerializable
    {
        public Selector()
        {
            Font = new();
        }
        
        public Selector(double? fontSize, string? fontWeight, string? activeColor) : this()
        {
            Font = new(fontSize, fontWeight);
            
            if (!string.IsNullOrWhiteSpace(activeColor))
                ActiveColor = new(activeColor);
        }

        public Font Font { get; set; }
        public Color? ActiveColor { get; set; }

        public Dictionary<string, string> Serialize()
        {
            return ActiveColor == null 
                ? Font.Serialize()
                : Font.Concat(ActiveColor);
        }
    } 
}
