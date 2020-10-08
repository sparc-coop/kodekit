using System.ComponentModel.DataAnnotations;

namespace UI_Kit.Model
{
    public class Kit
    {
        [Required]
        [StringLength(300, ErrorMessage = "Name is too long.")]
        public string Name { get; set; }
        public string Input { get; set; }
        public string Textarea { get; set; }
        public string H1 { get; set; }
        public string H2 { get; set; }
    }
}
