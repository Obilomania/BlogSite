using System.ComponentModel.DataAnnotations;

namespace BlogSite.Models
{
    public class BlogPost
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Image")]
        public string? ImageUrl { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        [Display(Name = "Posted On")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
