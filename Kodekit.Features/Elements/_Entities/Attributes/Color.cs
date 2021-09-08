﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace Kodekit.Features.Elements
{
    public class Color
    {
        public Color(string hex)
        {
            if (hex.Length < 6 || hex.Length > 7 || !hex.All("#0123456789abcdefABCDEF".Contains))
                throw new ArgumentOutOfRangeException(nameof(hex), "Hex value is not valid");

            if (hex.Length == 6 && !hex.StartsWith("#"))
                hex = "#" + hex;
            
            HexValue = hex;
        }

        public Color(System.Drawing.Color color)
        {
            HexValue = ColorTranslator.ToHtml(color);
        }

        public string HexValue { get; set; }

        public override string ToString() => $"{HexValue}";

        public Dictionary<string, Color> GenerateWeights(ColorTypes colorType)
        {
            var type = colorType.ToString().ToLower();
            
            return new Dictionary<string, Color>
            {
                { $"{type}-50", ChangeBrightness(0.52f) },
                { $"{type}-100", ChangeBrightness(0.37f) },
                { $"{type}-200", ChangeBrightness(0.26f) },
                { $"{type}-300", ChangeBrightness(0.12f) },
                { $"{type}-400", ChangeBrightness(0.06f) },
                { $"{type}-500", this },
                { $"{type}-600", ChangeBrightness(-0.06f) },
                { $"{type}-700", ChangeBrightness(-0.12f) },
                { $"{type}-800", ChangeBrightness(-0.18f) },
                { $"{type}-900", ChangeBrightness(-0.24f) }
            };
        }

        public Dictionary<string, Color> GenerateWeights(Color color)
        {
            var type = "grey";
            var colors = new Dictionary<string, Color>();
            
            colors.Add($"{type}-50", Interpolate(color, 0.05f));

            for (var i = 0.1f; i < 1f; i += 0.1f)
                colors.Add($"{type}-{Math.Round(i * 1000)}", Interpolate(color, i));

            return colors;
        }

        private Color ChangeBrightness(float correctionFactor)
        {
            var color = ColorTranslator.FromHtml(HexValue);
            
            float red = color.R;
            float green = color.G;
            float blue = color.B;

            if (correctionFactor < 0)
            {
                correctionFactor = 1 + correctionFactor;
                red *= correctionFactor;
                green *= correctionFactor;
                blue *= correctionFactor;
            }
            else
            {
                red = (255 - red) * correctionFactor + red;
                green = (255 - green) * correctionFactor + green;
                blue = (255 - blue) * correctionFactor + blue;
            }

            color = System.Drawing.Color.FromArgb(color.A, (int)red, (int)green, (int)blue);
            return new Color(color);
        }

        private Color ChangeSaturation(double saturation)
        {
            var c = ColorTranslator.FromHtml(HexValue);
            
            // Values from: https://www.w3.org/TR/filter-effects-1/#feColorMatrixElement , type="saturate"
            var s = saturation;
            
            static int clamp(double i) => Math.Min(255, Math.Max(0, Convert.ToInt32(i)));
            
            c = System.Drawing.Color.FromArgb(255,
               clamp((0.213 + 0.787 * s) * c.R + (0.715 - 0.715 * s) * c.G + (0.072 - 0.072 * s) * c.B),
               clamp((0.213 - 0.213 * s) * c.R + (0.715 + 0.285 * s) * c.G + (0.072 - 0.072 * s) * c.B),
               clamp((0.213 - 0.213 * s) * c.R + (0.715 - 0.715 * s) * c.G + (0.072 + 0.928 * s) * c.B));

            return new Color(c);
        }

        private Color Interpolate(Color secondColor, float percentage)
        {
            var color1 = ColorTranslator.FromHtml(HexValue);
            var color2 = ColorTranslator.FromHtml(secondColor.HexValue);
            
            double resultRed = color1.R + percentage * (color2.R - color1.R);
            double resultGreen = color1.G + percentage * (color2.G - color1.G);
            double resultBlue = color1.B + percentage * (color2.B - color1.B);

            var color = System.Drawing.Color.FromArgb((int)resultRed, (int)resultGreen, (int)resultBlue);

            return new Color(color);
        }
    }

    public enum ColorTypes
    {
        Primary,
        Secondary,
        Tertiary,
        Darkest,
        Lightest,
        Error,
        Warning,
        Success
    }
}
