using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Kodekit.Core
{
    public class Kit
    {
        [Key]
        public int Id { get; set; }
        public int? UserId { get; set; }
        public Guid? GUID { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string Name { get; set; }
        public string PrimaryColor { get; set; }
        public string SecondaryColor { get; set; }
        public string TertiaryColor { get; set; }
        public string DarkColor { get; set; }
        public string LightColor { get; set; }
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
        public bool? IsFinished { get; set; }

        public Kit(int userId, Guid guid, string name)
        {
            UserId = userId;
            GUID = guid;
            Name = name;
        }
    }
}
