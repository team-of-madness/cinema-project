using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using cinema_project.Data;
using cinema_project.Models;
using System;

namespace cinema_project.Controllers
{
	public class MoviesController : Controller
	{
		private readonly ApplicationDbContext _context;

		public MoviesController(ApplicationDbContext context)
		{
			_context = context;
		}

		// GET: Movies
		public async Task<IActionResult> Index()
		{
			var movies = await _context.Movies.Include(m => m.Genre).ToListAsync();
			/*if (TempData.ContainsKey("ErrorMessage"))
			{
				ViewBag.ErrorMessage = TempData["ErrorMessage"];
			}*/
			return View(movies);
		}

		// GET: Movies/CreateOrEdit
		public async Task<IActionResult> CreateOrEdit(int? id = null)
		{
			ViewData["GenreId"] = new SelectList(_context.Genre, "Id", "Id");
			IEnumerable<Genre> genres = _context.Genre;
			ViewBag.Genre = genres;

			Movie? movie = null;
			if (id == null)
			{
				movie = new Movie();
			}
			else
			{
				movie = await _context.Movies.FindAsync(id);
				if (movie == null)
				{
					return NotFound();
				}
			}
			return PartialView("_AddMoviesPartialView", movie);
		}

		// POST: Movies/CreateOrEdit
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> CreateOrEdit(Movie movie)
		{
			ViewData["GenreId"] = new SelectList(_context.Genre, "Id", "Id", movie.GenreId);
			IEnumerable<Genre> genres = _context.Genre;
			ViewBag.Genre = genres;
			var existMovie = _context.Movies.FirstOrDefaultAsync(m => m.Name == movie.Name);
			if (movie.Id == 0)
			{
				if (existMovie.Result == null)
				{
					if (ModelState.IsValid)
					{
						//TempData["SuccessMessage"] = "Element was successfully added";
						_context.Add(movie);
					}
					else
					{
						//TempData["ErrorMessage"] = "Please enter valid movie information.";
						return Json(new { success = true, movie });
					}
				}
				else
				{
					//Movie name already reserved
					//TempData["ErrorMessage"] = "This movie_name already exists!";
					//return RedirectToAction("Index");
					return Json(new { success = false, message = "This movie_name already exists!" });
				}
			}
			else
			{
				if (existMovie.Result == null)
				{
					if (ModelState.IsValid)
					{
						//TempData["SuccessMessage"] = "Element was successfully updated";
						_context.Update(movie);
					}
					else
					{
						//TempData["ErrorMessage"] = "Please enter valid movie information.";
						return Json(new { success = true, movie });
					}
				}
				else
				{
					//Movie name already reserved
					//TempData["ErrorMessage"] = "This movie_name already exists!";
					//return RedirectToAction("Index");
					return Json(new { success = false, message = "This movie_name already exists!" });
				}
			}
			await _context.SaveChangesAsync();
			return Json(new { success = true, message = "Index" });
		}

		// GET: Movies/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null || _context.Movies == null)
			{
				return NotFound();
			}

			var movie = await _context.Movies
				.Include(m => m.Genre)
				.FirstOrDefaultAsync(m => m.Id == id);
			if (movie == null)
			{
				return NotFound();
			}

			return PartialView("_MovieDetailPartialView", movie);
		}
		// GET: Movies/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null || _context.Movies == null)
			{
				return NotFound();
			}

			var movie = await _context.Movies
				.Include(m => m.Genre)
				.FirstOrDefaultAsync(m => m.Id == id);
			if (movie == null)
			{
				return NotFound();
			}

			return PartialView("_DeleteMoviePartialView", movie);
		}

		// POST: Movies/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			if (_context.Movies == null)
			{
				return Problem("Entity set 'ApplicationDbContext.Movies'  is null.");
			}
			var movie = await _context.Movies.FindAsync(id);
			if (movie != null)
			{
				_context.Movies.Remove(movie);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}
	}
}
