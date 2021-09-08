using Kodekit.Features.Elements;
using Sparc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kodekit.Features
{
    public class Kit : Root<string>
    {
        public Kit()
        {
            Id = Guid.NewGuid().ToString();
            KitId = Guid.NewGuid().ToString();

            Colors = new();

            HeadingSettings = new HeadingTypography();
            ParagraphSettings = new Typography("p");
            ButtonSettings = new Button();
            InputSettings = new Input();
            SelectorSettings = new Selector();
        }

        public void SetColor(ColorTypes colorType, string hexValue, string? toHexValue = null)
        {
            var colors = hexValue == null
                ? new Color(hexValue).GenerateWeights(colorType)
                : new Color(hexValue).GenerateWeights(new Color(toHexValue));

            Colors.RemoveAll(x => colors.ContainsKey(x.InternalName));
            
            foreach (var color in colors)
                Colors.Add(new Variable<Color>(colorType.ToString(), color.Key, color.Value));
        }

        public string KitId { get; set; }
        public string UserId { get; set; }
        public string ParentId { get; set; }//For child elements/later versions
        public string Description { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public List<Variable<Color>> Colors { get; set; }
        public HeadingTypography HeadingSettings { get; set; }
        public Typography ParagraphSettings { get; set; }
        public Button ButtonSettings { get; set; } 
        public Input InputSettings { get; set; }
        public Selector SelectorSettings { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsPublished { get; set; }
        public bool? IsAutoPublish { get; set; }
        public bool? IsDeleted { get; set; }

        public string ToCss()
        {
            var css = new StringBuilder();

            var headings = HeadingSettings.Expand();
            foreach (var heading in headings)
                Write(css, heading);

            Write(css, ParagraphSettings);
            Write(css, ButtonSettings);
            Write(css, InputSettings);
            Write(css, SelectorSettings);

            return css.ToString();
        }

        private StringBuilder Write(StringBuilder css, Element element)
        {
            return element.ToCss(css).AppendLine();
        }
    }
}
