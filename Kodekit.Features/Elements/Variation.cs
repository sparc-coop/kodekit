namespace Kodekit.Features.Elements
{
    public abstract class Variation : Element
    {
        public Variation(Element element, string name) 
            : base($"{element.Name}.{name}") 
        { }
        
    }
}
