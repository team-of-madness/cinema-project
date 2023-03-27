using cinema_project.Data;
using cinema_project.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace cinema_project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
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

        public IActionResult Index()
        {
            //IEnumerable<Movie> movies = _dbContext.Movies.ToList();
            //return View(movies);
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> ChooseSession(int? Id)
        {
            
            IEnumerable<Session> sessions = _dbContext.Sessions.Where(item => item.Id == Id);
            return View(sessions);
        }

        [HttpGet]
        public async Task<ActionResult> ChoosePlace(int? Id)
        {

            IEnumerable<Place> places = _dbContext.Places.Where(item => item.PlaceId == Id);
            return View(places);
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