using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using cinema_project.Data;
using cinema_project.Models;
using System;

namespace cinema_project.Controllers
{
    public class TicketsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TicketsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var tickets = await _context.Tickets.Include(t => t.Seat).Include(t => t.Session).ToListAsync();
            return View(tickets);
        }

        public async Task<IActionResult> CreateOrEdit(int? id = null)
        {
            ViewData["PlaceId"] = new SelectList(_context.Seats, "Id", "Id");
            ViewData["SessionId"] = new SelectList(_context.Sessions, "Id", "Id");

            Ticket? ticket = null;
            if (id == null)
            {
                ticket = new Ticket();
            }
            else
            {
                ticket = await _context.Tickets.FindAsync(id);
                if (ticket == null)
                {
                    return NotFound();
                }
            }
            return PartialView("_AddTicketPartialView", ticket);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateOrEdit(Ticket ticket)
        {
            ViewData["PlaceId"] = new SelectList(_context.Seats, "Id", "Id", ticket.PlaceId);
            ViewData["SessionId"] = new SelectList(_context.Sessions, "Id", "Id", ticket.SessionId);

            if (ticket.Id == 0)
            {
                if (ModelState.IsValid)
                {
                    _context.Add(ticket);
                }
                else
                {
                    return PartialView("_AddTicketPartialView", ticket);
                }
            }
            else
            {
                if (ModelState.IsValid)
                {
                    _context.Update(ticket);
                }
                else
                {
					return PartialView("_AddTicketPartialView", ticket);
				}
            }
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: Tickets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Tickets == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets
                .Include(t => t.Seat)
                .Include(t => t.Session)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return PartialView("_TicketDetailPartialView", ticket);
        }

        // GET: Tickets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Tickets == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets
                .Include(t => t.Seat)
                .Include(t => t.Session)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return PartialView("_DeleteTicketPartialView", ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Tickets == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Tickets'  is null.");
            }
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket != null)
            {
                _context.Tickets.Remove(ticket);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
