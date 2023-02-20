using BlogSite.Models;
using BlogSite.Models.ViewModel.Comment;
using BlogSite.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace BlogSite.Controllers
{
    public class CommentController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly IBlogPostRepository _blogRepository;
        public CommentController(IPostRepository postRepository, IBlogPostRepository blogRepository)
        {
            _postRepository = postRepository;
            _blogRepository = blogRepository;
        }

        public IActionResult Index(int id)
        {
            var comment = _postRepository.GetById(id);
            var model = new CommentIndexVM
            {
                Id = comment.Id,
                Comment = comment.Result.Comment,
                CommenterEmail = comment.Result.CommenterEmail, 
                Date = comment.Result.Date,
            };
            return View(model);
        }

        public IActionResult Create(int id)
        {
            var blogPost = _blogRepository.GetById(id);

            var model = new CreateCommentVM
            {
                BlogPostId= blogPost.Id,
                BlogPostTitle = blogPost.Result.Title,
                BlogPostDesc = blogPost.Result.Content,
                BlogPostCreated = blogPost.Result.Date,
                BlogPostImage = blogPost.Result.ImageUrl,
                BlogPostComments = blogPost.Result.Comments,
            };
            return View(model);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCommentVM model)
        {
            var blogPost = await _blogRepository.GetById(model.BlogPostId);
            var post = new Comments
            {
                Comment = model.Comment,
                CommenterEmail= model.CommenterEmail,
                Date = DateTime.Now,
                BlogPost = blogPost,
            };
            _postRepository.Add(post);
            return RedirectToAction("Index", "Post", post.Id);
        }
    }
}
