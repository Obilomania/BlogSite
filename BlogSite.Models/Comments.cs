using System.ComponentModel.DataAnnotations;


namespace BlogSite.Models
{
    public class Comments
    {
        [Key]
        public int Id { get; set; }

        public virtual BlogPost? BlogPost { get; set; }
        public string ApplicationUserId { get; set; }

        public string Commenter { get; set; }

        public string Comment { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;

    }
}
