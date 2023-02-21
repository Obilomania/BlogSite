using BlogSite.DataAccess;
using BlogSite.DataAccess.Repository.IRepository;
using BlogSite.Models;
using BlogSite.Models.ViewModel.Comment;
using Microsoft.AspNetCore.Mvc;

namespace BlogSite.Areas.User.Controllers
{
    [Area("User")]
    public class CommentController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly IBlogPostRepository _blog;
        private readonly ApplicationDbContext _context;
        public CommentController(IPostRepository postRepository, IBlogPostRepository blog, ApplicationDbContext context)
        {
            _postRepository = postRepository;
            _blog = blog;
            _context = context;
        }

        public IActionResult Create(int id)
        {
            var blogPost = _blog.GetByIdTracking(id);
            //var blogPost = _context.BlogPosts.First(x => x.Id == id);

            var model = new CreateCommentVM
            {
                BlogPostId = blogPost.Id,
                BlogPostTitle = blogPost.Result.Title,
                BlogPostDesc = blogPost.Result.Content,
                BlogPostCreated = blogPost.Result.Date,
                BlogPostImage = blogPost.Result.ImageUrl,
                BlogPostComments = blogPost.Result.Comments.ToList(),
            };
            return View(model);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCommentVM model, int id)
        {
            var blogPost = await _blog.GetByIdTracking(id);
            var post = new Comments
            {
                Comment = model.Comment,
                CommenterEmail = model.CommenterEmail,
                Date = DateTime.Now,
                BlogPost = blogPost,
            };
            _postRepository.Add(post);
            return RedirectToAction("Create", "Comment", post.Id);
        }
    }
}
