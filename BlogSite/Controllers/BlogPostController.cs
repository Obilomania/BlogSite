using BlogSite.Data;
using BlogSite.Models;
using BlogSite.Models.ViewModel;
using BlogSite.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace BlogSite.Controllers
{
    public class BlogPostController : Controller
    {
        private readonly IBlogPostRepository _context;
        private readonly IPhotoService _photoService;

        public BlogPostController(IBlogPostRepository context, IPhotoService photoService)
        {
            _context = context;
            _photoService = photoService;
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
        public IActionResult CreatePost()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePost(CreatePostVM createPostVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(createPostVM.ImageUrl);
                var post = new BlogPost()
                {
                    Id = createPostVM.Id,
                    Title = createPostVM.Title,
                    Content = createPostVM.Content,
                    ImageUrl = result.Url.ToString(),
                    Date = createPostVM.Date
                };
                _context.Add(post);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo Upload Failed");
            }
            return View(createPostVM);

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
            var postVM = new EditPostVM()
            {
                Id = id,
                Title = post.Title,
                Content = post.Content,
                URL = post.ImageUrl
            };
            return View(postVM);
        }


        [HttpPost]
        public async Task<IActionResult> EditPost(int id, EditPostVM editPostVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit club");
                return View("Edit", editPostVM);
            }
            var post = await _context.GetById(id);
            if (post != null)
            {
                try
                {
                    await _photoService.DeletePhotoAsync(post.ImageUrl);
                }
                catch
                {

                    ModelState.AddModelError("", "Could not delete photo");
                    return View(editPostVM);
                }
                var photoResult = await _photoService.AddPhotoAsync(editPostVM.ImageUrl);
                var postEdit = new BlogPost
                {
                    Id = id,
                    Title = editPostVM.Title,
                    Content = editPostVM.Content,
                    ImageUrl = photoResult.Url.ToString()
                };
                _context.Update(postEdit);
                return RedirectToAction("Index");
            }
            return View(editPostVM);
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
            _context.Delete(post);
            return RedirectToAction("Index");

        }
    }
}
