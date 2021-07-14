using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodekit.Features.Elements
{
    public class Input : Element
    {
        public Input() : base("input")
        {
        }

        [Css("font-size")]
        public Size FontSize { get; set; }
        [Css("font-weight")]
        public Weight FontWeight { get; set; }
        [Css("padding-top", "padding-bottom")]
        public Size VerticalPadding { get; set; }
        [Css("padding-left", "padding-right")]
        public Size HorizontalPadding { get; set; }
        [Css("border-radius")]
        public Size CornerRadius { get; set; }
        [Css("border-width")]
        public Size BorderWidth { get; set; }
    }
}
