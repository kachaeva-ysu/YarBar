using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using YarBar.Models;

namespace YarBar.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationContext _context;

        public HomeController(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(int typeId = 0)
        {
            var places = typeId == 0 ?
                _context.Places.Include(p => p.PlaceType).Include(p => p.Reviews) :
                _context.Places.Include(p => p.PlaceType).Include(p => p.Reviews).Where(p => p.PlaceTypeId == typeId);
            
            ViewBag.PlaceTypes = GetPlaceTypes();
            return View(await places.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var place = await _context.Places.Include(p => p.PlaceType).Include(p => p.Reviews)
                .FirstOrDefaultAsync(p => p.Id == id);
            
            if (place == null)
                return NotFound();
            
            return View(place);
        }

        public async Task<IActionResult> LeaveReview(int? id)
        {
            if (id == null)
                return NotFound();

            var place = await _context.Places.Include(p => p.PlaceType).Include(p => p.Reviews)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (place == null)
                return NotFound();

            ViewBag.Place = place;

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LeaveReview([Bind("PlaceId,Score,Comment")] Review review)
        {
            review.UserId = 1;
            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", new { id = review.PlaceId });
        }

        public async Task<IActionResult> Search(string searchString)
        {
            var results = new List<Place>();
            if(!string.IsNullOrEmpty(searchString))
            {
                var rg = new Regex(@"\w+", RegexOptions.IgnoreCase);
                var matches = rg.Matches(searchString.ToLower());
                foreach (var word in matches)
                {
                    var list = await _context.Places.Include(p => p.PlaceType).Include(p => p.Reviews)
                                              .Where(p => p.Name.Contains(word.ToString()) ||
                                                    p.Description.Contains(word.ToString()) ||
                                                    p.PlaceType.Name.Contains(word.ToString())).ToListAsync();
                    results.AddRange(list);
                }
                results = (from item in results select item).Distinct().ToList();
            }
            else
                return RedirectToAction("Index");

            ViewBag.PlaceTypes = GetPlaceTypes();
            return View("Index", results);
        }

        private SelectList GetPlaceTypes()
        {
            var categories = _context.PlaceTypes.ToList();
            categories.Insert(0, new PlaceType { Name = "Все", Id = 0 });
            SelectList list = new SelectList(categories, "Id", "Name");
            return list;
        }
    }
}
