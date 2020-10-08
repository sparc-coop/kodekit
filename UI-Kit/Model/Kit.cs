using System;
using System.ComponentModel.DataAnnotations;

namespace UI_Kit.Model
{
    public class Kit
    {
        [Required]
        [StringLength(300, ErrorMessage = "Name is too long.")]
        public string Name { get; set; }
        //public string Input { get; set; }
        //public string Textarea { get; set; }
        //public string H1 { get; set; }
        //public string H2 { get; set; }

        [Key]
        public int id { get; set; }
        public int UserId { get; set; }
        public Guid GUID { get; set; }
        public string Description { get; set; }
        //public string FileName { get; set; }
        public string Url { get; set; }
        //public string GUID { get; set; }

        //public Kit()
        //{

        //}

    //    public Kit(int userId, string name, string url)
    //    {
    //        //KitGUID = Guid.NewGuid();
    //        UserId = userId;
    //        Name = name;
    //    }
    }
}
