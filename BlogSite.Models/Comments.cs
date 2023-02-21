using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogSite.Models
{
    public class Comments
    {
        [Key]
        public int Id { get; set; }

        public virtual BlogPost? BlogPost { get; set; }

        public string CommenterEmail { get; set; }

        public string Comment { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

    }
}
