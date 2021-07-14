using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodekit.Features.Elements
{
    public class Button : Element
    {
        public Button() : base("button")
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
        public Size IconWidth { get; set; }
        public Size IconHeight { get; set; }

    }
}
