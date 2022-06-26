using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using YarBar.Models;

namespace YarBar.Controllers
{
    [Authorize(Roles = "manager")]
    public class PlaceTypesController : Controller
    {
        private readonly ApplicationContext _context;

        public PlaceTypesController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: PlaceTypes
        public async Task<IActionResult> Index()
        {
              return _context.PlaceTypes != null ? 
                          View(await _context.PlaceTypes.ToListAsync()) :
                          Problem("Entity set 'ApplicationContext.PlaceTypes'  is null.");
        }

        // GET: PlaceTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PlaceTypes == null)
            {
                return NotFound();
            }

            var placeType = await _context.PlaceTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (placeType == null)
            {
                return NotFound();
            }

            return View(placeType);
        }

        // GET: PlaceTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PlaceTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] PlaceType placeType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(placeType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(placeType);
        }

        // GET: PlaceTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PlaceTypes == null)
            {
                return NotFound();
            }

            var placeType = await _context.PlaceTypes.FindAsync(id);
            if (placeType == null)
            {
                return NotFound();
            }
            return View(placeType);
        }

        // POST: PlaceTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] PlaceType placeType)
        {
            if (id != placeType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(placeType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlaceTypeExists(placeType.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(placeType);
        }

        // GET: PlaceTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PlaceTypes == null)
            {
                return NotFound();
            }

            var placeType = await _context.PlaceTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (placeType == null)
            {
                return NotFound();
            }

            var places = _context.Places.Where(p => p.PlaceTypeId == id).Select(p => p.Name).ToList();
            if (places.Count()>0)
            {
                ViewBag.TypeName = placeType.Name;
                return View("DeleteError", places);
            }

            return View(placeType);
        }

        // POST: PlaceTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PlaceTypes == null)
            {
                return Problem("Entity set 'ApplicationContext.PlaceTypes'  is null.");
            }
            var placeType = await _context.PlaceTypes.FindAsync(id);
            if (placeType != null)
            {
                _context.PlaceTypes.Remove(placeType);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlaceTypeExists(int id)
        {
          return (_context.PlaceTypes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
