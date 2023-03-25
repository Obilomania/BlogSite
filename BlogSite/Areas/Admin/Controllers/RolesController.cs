using BlogSite.DataAccess;
using BlogSite.Models;
using BlogSite.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace BlogSite.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class RolesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }




        public IActionResult Index()
        {
            var roles = _context.Roles.ToList();
            return View(roles);
        }


        public IActionResult Upsert(string id)
        {

            if (string.IsNullOrEmpty(id))
            {
                return View();
            }
            else
            {
                var objFromDb = _context.Roles.FirstOrDefault(u => u.Id == id);
                return View(objFromDb);
            }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(IdentityRole roleObj)
        {
            if (await _roleManager.RoleExistsAsync(roleObj.Name))
            {
                //error
                TempData[SD.Error] = "Role Already Exist";
                return RedirectToAction(nameof(Index));
            }
            if (string.IsNullOrEmpty(roleObj.Id))
            {
                //create
                await _roleManager.CreateAsync(new IdentityRole() { Name = roleObj.Name });
                TempData[SD.Success] = "Role Created successfully";
            }
            else
            {
                //update
                var objRoleFromDb = _context.Roles.FirstOrDefault(u => u.Id == roleObj.Id);
                if (objRoleFromDb == null)
                {
                    TempData[SD.Error] = "Role Not Found!!";
                    return RedirectToAction(nameof(Index));
                }
                objRoleFromDb.Name = roleObj.Name;
                objRoleFromDb.NormalizedName = roleObj.Name.ToUpper();
                var result = await _roleManager.UpdateAsync(objRoleFromDb);
                TempData[SD.Success] = "Role Updated successfully";
            }
            return RedirectToAction(nameof(Index));

        }

        //DELETE ROLE CONTROLLER
        [HttpPost]

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            var objFromDb = _context.Roles.FirstOrDefault(u => u.Id == id);
            if (objFromDb == null)
            {
                return RedirectToAction(nameof(Index));
            }
            var userRolesForThisRole = _context.UserRoles.Where(u => u.RoleId == id).Count();
            if (userRolesForThisRole > 0)
            {
                return RedirectToAction(nameof(Index));
            }
            await _roleManager.DeleteAsync(objFromDb);
            TempData[SD.Error] = "Role Deleted successfully";
            return RedirectToAction(nameof(Index));

        }

    }
}
