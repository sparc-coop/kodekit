using System.Collections.Generic;

namespace Kodekit.Features.Elements
{
    public abstract class Element
    {
        public Element(string name) => Name = name;
        
        public string Name { get; set; }

        public Dictionary<string, string> Attributes { get; set; }
    }
}
