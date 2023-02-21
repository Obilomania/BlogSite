namespace BlogSite.Models.ViewModel.Comment
{
    public class CreateCommentVM
    {
        public string BlogPostTitle { get; set; }
        public string BlogPostDesc { get; set; }
        public string BlogPostImage { get; set; }
        public DateTime BlogPostCreated { get; set; }
        public int BlogPostId { get; set; }
        public string CommenterEmail { get; set; }
        public string Comment { get; set; }
        public ICollection<Comments> BlogPostComments { get; set; }
    }
}
