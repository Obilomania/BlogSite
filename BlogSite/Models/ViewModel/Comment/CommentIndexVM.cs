using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BlogSite.Models.ViewModel.Comment
{
    public class CommentIndexVM
    {
        public int Id { get; set; }

        public string CommenterEmail { get; set; }

        public string Comment { get; set; }


        [Display(Name = "Commented On")]
        public DateTime Date { get; set; } = DateTime.Now;
    }
}
