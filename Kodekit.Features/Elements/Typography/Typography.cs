namespace Kodekit.Features.Elements
{
    public class Typography : Element
    {
        public Typography() : base("p")
        {
        }

        public Typography(string name, string fontFamily, string fontWeight, double? fontSize, double? typeScale, double? lineHeight) : base(name)
        {
            FontFamily = new FontFamily(fontFamily);
            FontWeight = new Weight(fontWeight);
            FontSize = new Size(fontSize);
            TypeScale = new TypeScale(typeScale);
            LineHeight = new Size(lineHeight);

        }

        [Css("font-family")]
        public FontFamily FontFamily { get; set; }
        [Css("font-weight")]
        public Weight FontWeight { get; set; }
        [Css("font-size")]
        public Size FontSize { get; set; }
        
        public TypeScale TypeScale { get; set; }
        [Css("line-height")]
        public Size LineHeight { get; set; }
    }
}
