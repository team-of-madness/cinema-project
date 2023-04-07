using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using cinema_project.Data;
using cinema_project.Models;

namespace cinema_project.Controllers
{
    public class HallsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HallsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var halls = await _context.Halls.ToListAsync();
            return View(halls);
        }

        public async Task<IActionResult> CreateOrEdit(int? id = null)
        {

            Hall? hall = null;
            if (id == null)
            {
                hall = new Hall();
            }
            else
            {
                hall = await _context.Halls.FindAsync(id);
                if (hall == null)
                {
                    return NotFound();
                }
            }
            return PartialView("_AddHallPartialView", hall);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrEdit(Hall hall)
        {
            if (hall.Id == 0)
            {
                _context.Add(hall);
            }
            else
            {
                _context.Update(hall);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        // GET: Halls/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Halls == null)
            {
                return NotFound();
            }

            var hall = await _context.Halls
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hall == null)
            {
                return NotFound();
            }

            return PartialView("_HallDetailPartialView", hall);
        }

        // GET: Halls/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Halls == null)
            {
                return NotFound();
            }

            var hall = await _context.Halls
                .FirstOrDefaultAsync(m => m.Id == id);
            if (hall == null)
            {
                return NotFound();
            }

            return PartialView("_DeleteHallPartialView", hall);
        }

        // POST: Halls/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Halls == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Halls'  is null.");
            }
            var hall = await _context.Halls.FindAsync(id);
            if (hall != null)
            {
                _context.Halls.Remove(hall);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HallExists(int id)
        {
          return (_context.Halls?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
