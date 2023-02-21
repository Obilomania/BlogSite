using BlogSite.DataAccess.Repository.IRepository;
using BlogSite.Models;
using BlogSite.Models.ViewModel.BlogPost;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogSite.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class BlogPostController : Controller
    {
        private readonly IBlogPostRepository _context;
        private readonly IPostRepository _postRepository;
        private readonly IWebHostEnvironment _hostEnvironment;

        public BlogPostController(IBlogPostRepository context,
            IPostRepository postRepository,
            IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _postRepository = postRepository;
            _hostEnvironment = hostEnvironment;
        }

        //CONTROLLER TO GET ALL POST FROM DB
        public async Task<IActionResult> Index()
        {
            IEnumerable<BlogPost> posts = await _context.GetAll();
            return View(posts);
        }

        //CONTROLLER TO GET THE DETAIL OF A POST
        public async Task<IActionResult> PostDetail(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var post = await _context.GetById(id);
            if (post == null)
            {
                return NotFound();
            }
            return View(post);
        }

        //CONTROLLER TO CREATE POST
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BlogPost posts, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"images\blogImages");
                    var extension = Path.GetExtension(file.FileName);

                    if (posts.ImageUrl != null)
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, posts.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }
                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    posts.ImageUrl = @"\images\blogImages\" + fileName + extension;
                }
                if (posts.Id == 0)
                {
                    _context.Add(posts);
                }
                _context.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(posts);

        }

        //CONTROLLER TO EDIT POST
        public async Task<IActionResult> EditPost(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var post = await _context.GetById(id);
            if (post == null)
            {
                return NotFound();
            }
            var postVM = new EditBlogPostVM()
            {
                Id = id,
                Title = post.Title,
                Content = post.Content,
                URL = post.ImageUrl
            };
            return View(postVM);
        }


        [HttpPost]
        public async Task<IActionResult> EditPost(int id, BlogPost post, IFormFile file)
        {
            if (id != post.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                string wwwRootPath = _hostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(wwwRootPath, @"images\blogImages");
                    var extension = Path.GetExtension(file.FileName);
                    using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    post.ImageUrl = @"\images\blogImages\" + fileName + extension;
                }
                _context.Update(post);
                _context.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(post);
        }



        //CONTROLLER TO DELETE POST
        public async Task<IActionResult> Delete(int id)
        {
            var post = await _context.GetById(id);
            if (post == null)
            {
                return View("Error");
            };
            return View(post);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePOST(int id)
        {
            var post = await _context.GetById(id);
            if (post == null) { NotFound(); }
            var oldImagePath = Path.Combine(_hostEnvironment.WebRootPath, post.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldImagePath))
            {
                System.IO.File.Delete(oldImagePath);
            }
            _context.Delete(post);
            return RedirectToAction("Index");
        }
    }
}
