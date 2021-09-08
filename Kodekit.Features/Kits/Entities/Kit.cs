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
            Shadows = new();

            Headings = new();
            Paragraphs = new();
            Buttons = new();
            Inputs = new();
            Selectors = new();
            Settings = new();
        }

        public void UpdateTypography(HeadingTypography headings, Typography paragraphs)
        {
            Headings = headings;
            Paragraphs = paragraphs;
        }

        public void UpdateSettings(KitSettings settings)
        {
            Settings = settings;
        }

        internal void UpdateInputs(Input input)
        {
            Inputs = input;
        }

        internal void UpdateDropdowns(Dropdown dropdowns)
        {
            Dropdowns = dropdowns;
        }

        public Color GetColor(ColorTypes colorType)
        {
            var weight = colorType switch
            {
                ColorTypes.Lightest => 100,
                ColorTypes.Darkest => 900,
                _ => 500
            };

            return Colors.FirstOrDefault(x => x.InternalName == $"{colorType.ToString().ToLower()}-{weight}")?.Value;
        }

        internal void UpdateButtons(Button buttons)
        {
            Buttons = buttons;
        }

        public Shadow GetShadow(string shadowSize)
        {
            return Shadows.FirstOrDefault(x => x.InternalName == shadowSize.ToLower())?.Value;
        }

        public void UpdateShadows(Shadow smallShadow, Shadow xLargeShadow)
        {
            Shadows.Clear();
            var shadows = smallShadow.Expand(xLargeShadow);

            foreach (var shadow in shadows)
                Shadows.Add(new Variable<Shadow>(shadow.Key, shadow.Key, shadow.Value));
        }

        public void UpdateColor(ColorTypes colorType, string hexValue)
        {
            Colors.RemoveAll(x => colorType.ToString() == x.InternalName);
            Colors.Add(new Variable<Color>(colorType.ToString(), new Color(hexValue)));
        }

        public string KitId { get; set; }
        public string UserId { get; set; }
        public string ParentId { get; set; }//For child elements/later versions
        public string Description { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public List<Variable<Color>> Colors { get; set; }
        public List<Variable<Shadow>> Shadows { get; set; }
        public HeadingTypography Headings { get; set; }
        public Typography Paragraphs { get; set; }
        public Button Buttons { get; set; } 
        public Input Inputs { get; set; }
        public Selector Selectors { get; set; }
        public KitSettings Settings { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsPublished { get; set; }
        public bool? IsAutoPublish { get; set; }
        public bool? IsDeleted { get; set; }
        public Dropdown Dropdowns { get; set; }

        public string ToCss()
        {
            var css = new StringBuilder();

            var headings = Headings.Expand();
            foreach (var heading in headings)
                Write(css, heading);

            Write(css, Paragraphs);
            Write(css, Buttons);
            Write(css, Inputs);
            Write(css, Selectors);

            return css.ToString();
        }

        private StringBuilder Write(StringBuilder css, Element element)
        {
            return element.ToCss(css).AppendLine().AppendLine();
        }
    }
}
