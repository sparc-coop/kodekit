using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodekit.Features.Elements
{
    public class Selector : Element
    {
        public Selector() : base("input[type=checkbox]")
        {
        }
        
        public Selector(double fontSize, string fontWeight, string activeColor) : this()
        {
            FontSize = new(fontSize);
            FontWeight = new(fontWeight);
            ActiveColor = new(activeColor);
        }

        [Css("font-size")]
        public Size FontSize { get; set; }
        [Css("font-weight")]
        public Weight FontWeight { get; set; }
        [Css("color")]
        public Color ActiveColor { get; set; }
    } 
}
