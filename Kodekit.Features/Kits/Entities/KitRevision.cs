using Kodekit.Features.Elements;
using Sparc.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kodekit.Features
{
    public class KitRevision : Root<string>
    {
        private KitRevision()
        {
            Id = string.Empty;
            KitId = string.Empty;
            DateCreated = DateTime.UtcNow;

            Colors = new();
            Shadows = new();

            Headings = new();
            Paragraphs = new();
            Buttons = new();
            Inputs = new();
            Selectors = new();
            Settings = new();
            Dropdowns = new();
        }


        public KitRevision(Kit kit) : this()
        {
            KitId = kit.Id;
            Id = "1";
        }

        public KitRevision(KitRevision revision) : this()
        {
            // Auto-incrementing integer version IDs
            Id = (int.Parse(revision.Id) + 1).ToString();

            KitId = revision.KitId;
            ParentRevisionId = revision.Id;

            Colors = revision.Colors;
            Shadows = revision.Shadows;
            Headings = revision.Headings;
            Paragraphs = revision.Paragraphs;
            Buttons = revision.Buttons;
            Inputs = revision.Inputs;
            Selectors = revision.Selectors;
            Settings = revision.Settings;
            Dropdowns = revision.Dropdowns;
        }

        public string KitId { get; set; }
        public DateTime DateCreated { get; set; }
        public string? ParentRevisionId { get; set; }//For child elements/later versions
        public string? Name { get; set; }

        // Kit settings
        public List<Variable<Color>> Colors { get; set; }
        public List<Variable<Shadow>> Shadows { get; set; }
        public Typography Headings { get; set; }
        public Typography Paragraphs { get; set; }
        public Button Buttons { get; set; }
        public Input Inputs { get; set; }
        public Selector Selectors { get; set; }
        public Settings Settings { get; set; }
        public Dropdown Dropdowns { get; set; }
        public bool IsDeleted { get; set; }

        public void UpdateSelectors(Selector selectors)
        {
            Selectors = selectors;
        }

        public void UpdateTypography(Typography headings, Typography paragraphs)
        {
            Headings = headings;
            Paragraphs = paragraphs;
        }

        public void UpdateSettings(Settings settings)
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

        public Color? GetColor(ColorTypes colorType)
        {
            return Colors.FirstOrDefault(x => x.Name == $"{colorType.ToString().ToLower()}")?.Value;
        }

        internal void UpdateButtons(Button buttons)
        {
            Buttons = buttons;
        }

        public Shadow? GetShadow(string shadowSize)
        {
            return Shadows.FirstOrDefault(x => x.Name == shadowSize.ToLower())?.Value;
        }

        public void UpdateShadows(Shadow smallShadow, Shadow xLargeShadow)
        {
            Shadows.Clear();
            var shadows = smallShadow.Expand(xLargeShadow);

            foreach (var shadow in shadows)
                Shadows.Add(new Variable<Shadow>(shadow.Key, shadow.Value));
        }

        public void UpdateColor(ColorTypes colorType, string? hexValue)
        {
            Colors.RemoveAll(x => colorType.ToString().ToLower() == x.Name.ToLower());

            if (hexValue != null)
                Colors.Add(new Variable<Color>(colorType.ToString(), new Color(hexValue)));
        }

        public List<string?> Imports()
        {
            return new List<string?>
            {
                Headings.Font.FamilyUrl,
                Paragraphs.Font.FamilyUrl,
                Buttons.Font.FamilyUrl,
                Inputs.Font.FamilyUrl,
                Selectors.Font.FamilyUrl
            }
            .Distinct()
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .ToList();
        }
    }
}
