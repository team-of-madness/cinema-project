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
        public async Task<IActionResult> ChooseSession(int? Id)//Movie id
        {
            var sessions_dbContext = _dbContext.Sessions.Where(item => item.Movie.Id == Id).Include(m => m.Movie).Include(h => h.Hall).Include(t => t.Tickets);
            return View(await sessions_dbContext.ToListAsync());
        }

        [HttpGet]
        public async Task<IActionResult> ChoosePlace(int? Id)//Session id
        {
            Session? sessionObject = _dbContext.Sessions.Find(Id);
            Hall? hallObject = _dbContext.Halls.FindAsync(sessionObject.HallId).Result;
            hallObject.Sessions = _dbContext.Sessions.Where(el => el.Id == Id).ToList();
            return View(hallObject);
        }

        public async Task<IActionResult> BuyTicket(int row, int column, int hallId, int sessionId)
        {
            var foundSeat = await _dbContext.Seats.Where(item => item.Row == row && item.Column == column && item.HallId == hallId).Include(h => h.Hall).FirstOrDefaultAsync();
            Ticket ticket = new Ticket();
            ticket.SessionId = sessionId;
            ticket.Session = await _dbContext.Sessions.FindAsync(sessionId);
            ticket.Session.Movie = await _dbContext.Movies.FindAsync(ticket.Session.MovieId);
            if(foundSeat == null)
            {
                Seat seat = new Seat();
                seat.Column = column;
                seat.Row = row;
                seat.HallId = hallId;
                _dbContext.Add(seat);
                await _dbContext.SaveChangesAsync();
                var contextSeat = await _dbContext.Seats.Where(item => item.Row == row && item.Column == column && item.HallId == hallId).Include(h => h.Hall).FirstOrDefaultAsync();
                contextSeat.Hall = await _dbContext.Halls.FindAsync(hallId);
                ticket.Seat = contextSeat;
                ViewBag.Hall = await _dbContext.Halls.FindAsync(hallId);
                ticket.PlaceId = contextSeat.Id;
                return PartialView("_BuyTicket", ticket);
            }
            else
            {
                foundSeat.Hall = await _dbContext.Halls.FindAsync(hallId);
                ticket.Seat = foundSeat;
                ticket.PlaceId = foundSeat.Id;
                return PartialView("_BuyTicket", ticket);
            }
        }

        [HttpPost]
        public async Task<IActionResult> BuyTicket(Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                ticket.Seat = null;
                ticket.Session = null;
                _dbContext.Add(ticket);
            }
            else
            {
                return Json(new { success = false, ticket, message = "Object ticket is not valid" });
            }
            await _dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
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