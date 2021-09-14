﻿using Kodekit.Features.Elements;
using Sparc.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kodekit.Features
{
    public class Kit : Root<string>
    {
        public Kit()
        {
            Id = Guid.NewGuid().ToString();

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

        public string? UserId { get; set; }
        public string? ParentId { get; set; }//For child elements/later versions
        public string? Description { get; set; }
        public string? Url { get; set; }
        public string? Name { get; set; }
        public List<Variable<Color>> Colors { get; set; }
        public List<Variable<Shadow>> Shadows { get; set; }
        public Typography Headings { get; set; }
        public Typography Paragraphs { get; set; }
        public Button Buttons { get; set; } 
        public Input Inputs { get; set; }
        public Selector Selectors { get; set; }
        public Settings Settings { get; set; }
        public DateTime? DateCreated { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool? IsPublished { get; set; }
        public bool? IsAutoPublish { get; set; }
        public bool? IsDeleted { get; set; }
        public Dropdown Dropdowns { get; set; }
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
