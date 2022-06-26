using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using YarBar.Models;

namespace YarBar.Controllers
{
    [Authorize]
    public class ReviewsController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ReviewsController(ApplicationContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var userName = HttpContext.User.Identity.Name;
            var user = await _userManager.FindByNameAsync(userName);
            var reviews = await _context.Reviews.Where(r => r.UserId==user.Id).Include(r => r.Place).ToListAsync();
            return View(reviews);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Reviews == null)
                return NotFound();

            var review = await _context.Reviews.Where(r => r.Id == id).Include(r => r.Place).FirstAsync();
            if (review == null)
                return NotFound();

            return View(review);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (id == null || _context.Reviews == null)
                return NotFound();

            var review = await _context.Reviews.FirstOrDefaultAsync(m => m.Id == id);
            if (review == null)
                return NotFound();

            _context.Reviews.Remove(review);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
