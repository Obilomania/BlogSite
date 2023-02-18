using System.ComponentModel.DataAnnotations;

namespace BlogSite.Models.ViewModel
{
    public class EditPostVM
    {
        public int Id { get; set; }
        [Display(Name = "Image")]
        public IFormFile? ImageUrl { get; set; }
        public string? URL { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
