using BlogSite.DataAccess.Repository.IRepository;
using BlogSite.Models;
using BlogSite.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace BlogSite.Areas.User.Controllers
{
    [Area("User")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly IPostRepository _postRepository;

        public HomeController(ILogger<HomeController> logger, IBlogPostRepository blogPostRepository, IPostRepository postRepository)
        {
            _logger = logger;
            _blogPostRepository = blogPostRepository;
            _postRepository = postRepository;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<BlogPost> posts = await _blogPostRepository.GetAll();

            return View(posts);
        }

        //CONTROLLER TO SEE POST DETAILS
        public async Task<IActionResult> PostDetail(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var post = await _blogPostRepository.GetById(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}