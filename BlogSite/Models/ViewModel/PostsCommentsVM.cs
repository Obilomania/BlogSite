using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BlogSite.Models.ViewModel
{
    public class PostsCommentsVM
    {
        public int Id { get; set; }

        public int BlogPostId { get; set; }

        [Required]
        [Display(Name = "Your Email Address")]
        public string CommenterEmail { get; set; }

        [Required]
        public string Comment { get; set; }


        [Display(Name = "Commented On")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
