using EventEase_website.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EventEase_website.Controllers
{
    public class EventController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var events = await _context.Event.Include(e => e.Venue).ToListAsync();
            return View(events);
        }

        public IActionResult Create()
        {
            ViewData["Venues"] = _context.Venue.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EventM @event)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@event);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Event created successfully.";
                return RedirectToAction(nameof(Index));
            }

            ViewData["Venues"] = _context.Venue.ToList();
            return View(@event);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var @event = await _context.Event.FindAsync(id);
            if (@event == null) return NotFound();

            ViewData["Venues"] = _context.Venue.ToList();
            return View(@event);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EventM @event)
        {
            if (id != @event.EventID) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(@event);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Event updated successfully.";
                return RedirectToAction(nameof(Index));
            }

            ViewData["Venues"] = _context.Venue.ToList();
            return View(@event);
        }

        // POE PART 2: Confirm Deletion (GET)
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var @event = await _context.Event
                .Include(e => e.Venue)
                .FirstOrDefaultAsync(e => e.EventID == id);

            if (@event == null) return NotFound();

            return View(@event);
        }

        // POE PART 2: Perform Deletion (POST) - Logic to restrict the deletion of events associated with active bookings.
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @event = await _context.Event.FindAsync(id);
            if (@event == null) return NotFound();

            var isBooked = await _context.Booking.AnyAsync(b => b.EventID == id);
            if (isBooked)
            {
                TempData["ErrorMessage"] = "Cannot delete event because it has existing bookings.";
                return RedirectToAction(nameof(Index));
            }

            _context.Event.Remove(@event);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Event deleted successfully.";
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Event
                .Include(e => e.Venue) // Include related venue if applicable
                .FirstOrDefaultAsync(m => m.EventID == id);

            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }
    }
}
