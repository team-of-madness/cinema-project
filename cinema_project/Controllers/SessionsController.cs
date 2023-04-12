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
    public class SessionsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public SessionsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var sessions = await _context.Sessions.Include(s => s.Hall).Include(s => s.Movie).ToListAsync();
            return View(sessions);
        }
        public async Task<IActionResult> CreateOrEdit(int? id = null)
        {
            ViewData["HallId"] = new SelectList(_context.Halls, "Id", "Id");
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Id");
            IEnumerable<Hall> halls = _context.Halls;
            ViewBag.Halls = halls;
            IEnumerable<Movie> movies = _context.Movies;
            ViewBag.Movies = movies;

            Session? session = null;
            if (id == null)
            {
                session = new Session();
            }
            else
            {
                session = await _context.Sessions.FindAsync(id);
                if (session == null)
                {
                    return NotFound();
                }
            }
            return PartialView("_AddSessionPartialView", session);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOrEdit(Session session)
        {
            ViewData["HallId"] = new SelectList(_context.Halls, "Id", "Id", session.HallId);
            ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "Id", session.MovieId);
			IEnumerable<Hall> halls = _context.Halls;
			ViewBag.Halls = halls;
			IEnumerable<Movie> movies = _context.Movies;
			ViewBag.Movies = movies;
			if (session.Id == 0)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(session);
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
                    _context.Update(session);
                }
                else
                {
                    return BadRequest("Not valid");
                }
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        // GET: Sessions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Sessions == null)
            {
                return NotFound();
            }

            var session = await _context.Sessions
                .Include(s => s.Hall)
                .Include(s => s.Movie)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (session == null)
            {
                return NotFound();
            }

            return PartialView("_SessionDetailPartialView", session);
        }

        // GET: Sessions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Sessions == null)
            {
                return NotFound();
            }

            var session = await _context.Sessions
                .Include(s => s.Hall)
                .Include(s => s.Movie)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (session == null)
            {
                return NotFound();
            }

            return PartialView("_DeleteSessionPartialView", session);
        }

        // POST: Sessions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Sessions == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Sessions'  is null.");
            }
            var session = await _context.Sessions.FindAsync(id);
            if (session != null)
            {
                _context.Sessions.Remove(session);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
