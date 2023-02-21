using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace BlogSite.Models
{
    public class BlogPost
    {
        [Key]
        public int Id { get; set; }
        [ValidateNever]
        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }


        public DateTime Date { get; set; } = DateTime.Now;

        public ApplicationUser? User { get; set; }

        public ICollection<Comments>? Comments { get; set; }
    }
}
