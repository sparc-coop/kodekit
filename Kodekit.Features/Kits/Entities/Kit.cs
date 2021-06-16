using Sparc.Core;
using System;

namespace Kodekit.Features
{
    public class Kit : Root<string>
    {
        public Kit()
        {
            Id = Guid.NewGuid().ToString();
            KitId = Id;
        }

        public string KitId { get; set; }
        public string UserId { get; set; }
        public string ParentId { get; set; }//For child elements/later versions
        public int Version { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public string PrimaryColor { get; set; }
        public string SecondaryColor { get; set; }
        public string TertiaryColor { get; set; }
        public string DarkColor { get; set; }
        public string LightColor { get; set; }
        public string ErrorColor { get; set; }
        public string WarningColor { get; set; }
        public string SuccessColor { get; set; }
        public string TypeScale { get; set; }
        public string HeaderFont { get; set; }
        public string HeaderFontSize { get; set; }
        public string HeaderTypeScale { get; set; }
        public string HeaderLineHeight { get; set; }
        public string BodyFont { get; set; }
        public string BodyFontSize { get; set; }
        public string BodyTypeScale { get; set; }
        public string BodyLineHeight { get; set; }
        public DateTime? DateCreated { get; set; }
        public bool? IsPublished { get; set; }
    }
}
