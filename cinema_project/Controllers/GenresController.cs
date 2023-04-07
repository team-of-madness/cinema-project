using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using cinema_project.Data;
using cinema_project.Models;

namespace cinema_project.Controllers
{
    public class GenresController : Controller
    {
        private readonly ApplicationDbContext _context;

        public GenresController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var genres = await _context.Genre.ToListAsync();
            return View(genres);
        }
        public async Task<IActionResult> CreateOrEdit(int? id = null)
        {

            Genre? genre = null;
            if (id == null)
            {
                genre = new Genre();
            }
            else
            {
                genre = await _context.Genre.FindAsync(id);
                if (genre == null)
                {
                    return NotFound();
                }
            }
            return PartialView("_AddGenrePartialView", genre);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrEdit(Genre genre)
        {
            if (genre.Id == 0)
            {
                _context.Add(genre);
            }
            else
            {
                _context.Update(genre);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Genre == null)
            {
                return NotFound();
            }

            var genre = await _context.Genre
                .FirstOrDefaultAsync(m => m.Id == id);
            if (genre == null)
            {
                return NotFound();
            }

            return PartialView("_GenreDetailPartialView", genre);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Genre == null)
            {
                return NotFound();
            }

            var genre = await _context.Genre
                .FirstOrDefaultAsync(m => m.Id == id);
            if (genre == null)
            {
                return NotFound();
            }

            return PartialView("_DeleteGenrePartialView", genre);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Genre == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Genre'  is null.");
            }
            var genre = await _context.Genre.FindAsync(id);
            if (genre != null)
            {
                _context.Genre.Remove(genre);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GenreExists(int id)
        {
          return (_context.Genre?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
