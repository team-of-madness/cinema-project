using cinema_project.Data;
using cinema_project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;

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

        public IActionResult CreateSeat()
        {
            return RedirectToAction("Index", "Seats");
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
            //Incorrect logic. Required to fix
            int sessionID = _dbContext.Sessions.FindAsync(Id).Result.HallId;
            var seats_dbContext = _dbContext.Seats.Include(h => h.Hall).Where(item => item.Hall.Id == sessionID);
            return View(await seats_dbContext.ToListAsync());
        }

        public async Task<IActionResult> BuyTicket(int row, int column)
        {
            // Code to search for products based on the query and category parameters
            // ...
            Seat seat = new Seat { Id = 100, Row = row, Column = column, HallId = 1 };
            // Return the search results to a view
            return PartialView("_BuyTicket", seat);
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