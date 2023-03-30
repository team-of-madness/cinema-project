using cinema_project.Data;
using cinema_project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace cinema_project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public HomeController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult CreateMovie()
        {
            return RedirectToAction("Index", "Movies");
        }
        public IActionResult CreateSession()
        {
            return RedirectToAction("Index", "Sessions");
        }
        public IActionResult CreateTicket()
        {
            return RedirectToAction("Index", "Tickets");
        }
        public IActionResult CreateGenre()
        {
            return RedirectToAction("Index", "Genres");
        }
        public IActionResult CreateHall()
        {
            return RedirectToAction("Index", "Halls");
        }

        public IActionResult CreatePlace()
        {
            return RedirectToAction("Index", "Places");
        }
        public async Task<IActionResult> Index()
        {
            var movies_dbContext = _dbContext.Movies.Include(g => g.Genre);
            return View(await movies_dbContext.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> ChooseSession(int? Id)
        {
            
            var sessions_dbContext = _dbContext.Sessions.Where(item => item.Movie.Id == Id).Include(m => m.Movie).Include(h => h.Hall).Include(t => t.Tickets);
            return View(await sessions_dbContext.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> ChoosePlace(int? Id)
        {

            var places_dbContext = _dbContext.Places.Where(item => item.PlaceId == Id);
            return View(await places_dbContext.ToListAsync());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}