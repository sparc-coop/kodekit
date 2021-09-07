using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodekit.Features.Elements
{
    public class Typography : Element
    {
        public Typography(string name) : base(name)
        {
        }

        [Css("font-family")]
        public FontFamily FontFamily { get; set; }
        [Css("font-weight")]
        public Weight FontWeight { get; set; }
        [Css("font-size")]
        public Size FontSize { get; set; }
        
        public Scale TypeScale { get; set; }
        [Css("line-height")]
        public Size LineHeight { get; set; }
    }
}
