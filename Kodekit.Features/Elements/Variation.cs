namespace Kodekit.Features.Elements
{
    public abstract class Variation : Element
    {
        public Element BaseElement { get; set; }

        public Variation(Element element, string name) : base(name) => BaseElement = element;
        
    }
}
