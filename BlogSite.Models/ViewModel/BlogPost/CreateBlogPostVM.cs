using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BlogSite.Models.ViewModel.BlogPost
{
    public class CreateBlogPostVM
    {
        public int Id { get; set; }
        [Display(Name = "Image")]
        public IFormFile? ImageUrl { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        [Display(Name = "Posted On")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
