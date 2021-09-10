namespace Kodekit.Features.Elements
{
    public record FontFamily
    {
        public FontFamily()
        { }
        
        public FontFamily(string fontFamily)
        {
            FriendlyName = fontFamily;
            InternalName = fontFamily;
            
            var cleanFamily = fontFamily.Replace(" ", "+");
            SourceUrl = $"https://fonts.googleapis.com/css?family={cleanFamily}";
        }

        public string FriendlyName { get; set; }
        public string InternalName { get; set; }
        public string SourceUrl { get; set; }

        public override string ToString() => $"'{InternalName}'";
    }
}
