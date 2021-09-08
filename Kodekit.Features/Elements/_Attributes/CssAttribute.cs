using System;
using System.Collections.Generic;
using System.Linq;

namespace Kodekit.Features.Elements
{
    public class CssAttribute : Attribute
    {
        public CssAttribute(string cssName, params string[] additionalCssNames)
        {
            Name = cssName;
            AdditionalCssNames = additionalCssNames.ToList();
        }

        public string Name { get; }
        public List<string> AdditionalCssNames { get; }
    }
}