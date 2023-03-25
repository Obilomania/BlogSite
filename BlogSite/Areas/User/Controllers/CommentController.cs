using BlogSite.DataAccess;
using BlogSite.DataAccess.Repository.IRepository;
using BlogSite.Models;
using BlogSite.Models.ViewModel.Comment;
using BlogSite.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogSite.Areas.User.Controllers
{
    [Area("User")]
    public class CommentController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly IBlogPostRepository _blog;
        private readonly ApplicationDbContext _context;
        private readonly  IHttpContextAccessor _contextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        public CommentController(IPostRepository postRepository,
            IBlogPostRepository blog,
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            IHttpContextAccessor contextAccessor)
        {
            _postRepository = postRepository;
            _blog = blog;
            _context = context;
            _userManager = userManager;
            _contextAccessor = contextAccessor;
        }

        public IActionResult Create(int id)
        {
            var blogPost = _blog.GetByIdTracking(id);

            var model = new CreateCommentVM
            {
                
                //Commenter = _userManager.GetUserAsync(User).Result.NickName,
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
            var curUserId = _contextAccessor.HttpContext.User.GetUserId();

            var post = new Comments
            {
                ApplicationUserId = curUserId,
                Comment = model.Comment,
                Commenter = _userManager.GetUserAsync(User).Result.NickName,
                Date = DateTime.Now,
                BlogPost = blogPost,
            };
            _postRepository.Add(post);
            return RedirectToAction("Create", "Comment", post.Id);
        }
    }
}
