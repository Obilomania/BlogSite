using BlogSite.DataAccess.Repository.IRepository;
using BlogSite.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogSite.Areas.User.Controllers
{
    [Area("User")]
    public class MainBlogController : Controller
    {
        private readonly IBlogPostRepository _context;

        public MainBlogController(IBlogPostRepository context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<BlogPost> posts = await _context.GetAll();

            return View(posts);
        }
    }
}
