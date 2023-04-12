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
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOrEdit(Genre genre)
        {

            if (genre.Id == 0)
            {
                var existGenre = _context.Genre.FirstOrDefaultAsync(g => g.GenreName == genre.GenreName);
                if (existGenre.Result == null)
                {
                    if (ModelState.IsValid)
                    {
                        _context.Add(genre);
                    }
                    else
                    {
                        return PartialView("_AddGenrePartialView", genre);
                    }   
                }
                else 
                {
                    return BadRequest("This genre_name already exists!");
                }
            }
            else
            {
                if (ModelState.IsValid)
                {
                    _context.Update(genre);
                }
                else
                {
                    return PartialView("_AddGenrePartialView", genre);
                }
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
