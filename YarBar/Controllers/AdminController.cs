using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using YarBar.Models.ViewModels;

namespace YarBar.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var list = new List<UserWithRoles>();
            var users = _userManager.Users.ToList();
            foreach(var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                list.Add(new UserWithRoles { User=user, Roles=roles });
            }
            return View(list);
        }

        public async Task<IActionResult> DeleteUser(string id)
        {
            if (id == null)
                return NotFound();
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();
            var roles = await _userManager.GetRolesAsync(user);
            return View(new UserWithRoles { User = user, Roles = roles });
        }

        [HttpPost, ActionName("DeleteUser")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (id == null)
                return NotFound();
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound();
            await _userManager.DeleteAsync(user);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> EditUserRoles(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var roles = _roleManager.Roles.ToList();
                var model = new ChangeRoleViewModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    AllRoles = roles,
                    UserRoles = userRoles
                };
                return View(model);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> EditUserRoles(string id, List<string> roles)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var addedRoles = roles.Except(userRoles);
                var removedRoles = userRoles.Except(roles);
                await _userManager.AddToRolesAsync(user, addedRoles);
                await _userManager.RemoveFromRolesAsync(user, removedRoles);
                return RedirectToAction("Index");
            }
            return NotFound();
        }
        public IActionResult GetRoles()
        {
            return View(_roleManager.Roles.ToList());
        }
        public IActionResult CreateRole() => View();
        [HttpPost]
        public async Task<IActionResult> CreateRole(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                IdentityResult result = await _roleManager.CreateAsync(new IdentityRole(name));
                if (result.Succeeded)
                {
                    return RedirectToAction("GetRoles");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View(name);
        }

        public async Task<IActionResult> DeleteRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
                return NotFound();
            var users = await _userManager.GetUsersInRoleAsync(role.Name);
            if (users.Count > 0) 
            {
                ViewBag.RoleName = role.Name;
                var list = users.Select(u => u.Email).ToList();
                return View("DeleteError", list);
            }
            return View(role);
        }

        [HttpPost, ActionName("DeleteRole")]
        public async Task<IActionResult> DeleteRoleConfirmed(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
                return NotFound();
            await _roleManager.DeleteAsync(role);
            return RedirectToAction("GetRoles");
        }
    }
}
