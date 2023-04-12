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
            Session? sessionObject = _dbContext.Sessions.Find(Id);
            var seats_dbContext = _dbContext.Seats.Include(h => h.Hall).Where(item => item.HallId == sessionObject.HallId);
            return View(await seats_dbContext.ToListAsync());
        }

        public async Task<IActionResult> BuyTicket(int row, int column, int hallId)
        {
            var foundSeat = _dbContext.Seats.Where(item => item.Row == row && item.Column == column && item.HallId == hallId).Include(h => h.Hall).FirstOrDefaultAsync();
            if(foundSeat.Result == null)
            {
                Seat seat = new Seat();
                seat.Column = column;
                seat.Row = row;
                seat.HallId = hallId;
                _dbContext.Add(seat);
                await _dbContext.SaveChangesAsync();
                var contextSeat = _dbContext.Seats.Where(item => item.Row == row && item.Column == column && item.HallId == hallId).Include(h => h.Hall).FirstOrDefaultAsync();
                return PartialView("_BuyTicket", contextSeat.Result);
            }
            else
            {
                return PartialView("_BuyTicket", foundSeat.Result);
            }
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
        //:)
    }
}