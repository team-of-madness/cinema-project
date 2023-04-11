using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using cinema_project.Data;
using cinema_project.Models;

namespace cinema_project.Controllers
{
    public class SeatsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SeatsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Seats
        public async Task<IActionResult> Index()
        {
            var seats = await _context.Seats.Include(p => p.Hall).ToListAsync();
            return View(seats);
        }
        // GET: Seats/CreateOrEdit
        public async Task<IActionResult> CreateOrEdit(int? id = null)
        {
            ViewData["HallId"] = new SelectList(_context.Halls, "Id", "Id");
            IEnumerable<Hall> halls = _context.Halls;
            ViewBag.Halls = halls;

            Seat? seat = null;
            if (id == null)
            {
                seat = new Seat();
            }
            else
            {
                seat = await _context.Seats.FindAsync(id);
                if (seat == null)
                {
                    return NotFound();
                }
            }
            return PartialView("_AddSeatPartialView", seat);
        }
        // POST: Seats/CreateOrEdit

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOrEdit(Seat seat)
        {
            ViewData["HallId"] = new SelectList(_context.Halls, "Id", "Id", seat.HallId);
            if (seat.Id == 0)
            {
                var existingOne = _context.Seats.Where(s => s.Column == seat.Column && s.Row == seat.Row).FirstOrDefaultAsync();
                if (existingOne.Result != null)
                {
                    //If already exists - return Action("Index")
                    return RedirectToAction("Index");
                }
                if (ModelState.IsValid)
                {
                    _context.Add(seat);
                }
                else
                {
                    return BadRequest("Not valid");
                }
            }
            else
            {
                if (ModelState.IsValid)
                {
                    _context.Update(seat);
                }
                else
                {
                    return BadRequest("Not valid");
                }
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: Seats/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Seats == null)
            {
                return NotFound();
            }

            var place = await _context.Seats
                .Include(p => p.Hall)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (place == null)
            {
                return NotFound();
            }

            return PartialView("_SeatDetailPartialView", place);
        }

        // GET: Seats/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Seats == null)
            {
                return NotFound();
            }

            var seat = await _context.Seats
                .Include(p => p.Hall)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (seat == null)
            {
                return NotFound();
            }

            return PartialView("_DeleteSeatPartialView", seat);
        }

        // POST: Seats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Seats == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Seats'  is null.");
            }
            var seat = await _context.Seats.FindAsync(id);
            if (seat != null)
            {
                _context.Seats.Remove(seat);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlaceExists(int id)
        {
            return (_context.Seats?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
