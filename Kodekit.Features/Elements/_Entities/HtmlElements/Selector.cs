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

        [Css("font-size")]
        public Size FontSize { get; set; }
        [Css("font-weight")]
        public Weight FontWeight { get; set; }
        [Css("color")]
        public Color ActiveColor { get; set; }
    }
}
