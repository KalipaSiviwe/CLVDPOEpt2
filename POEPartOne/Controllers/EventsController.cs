using Microsoft.EntityFrameworkCore;
using POEPartOne.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace POEPartOne.Controllers
{
    public class EventsController : Controller
    {
        private readonly Poept1Context _context;

        public EventsController(Poept1Context context)
        {
            _context = context;
        }

        // GET: Events
        public async Task<IActionResult> Index()
        {
            var poept1Context = _context.Events.Include(e => e.Venue);
            return View(await poept1Context.ToListAsync());
        }

        // GET: Events/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Events/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EventId,Name,EventDate,VenueId,Description,ImageUrl")] Event @event)
        {
            // Validate Venue exists
            if (!_context.Venues.Any(v => v.VenueId == @event.VenueId))
            {
                ModelState.AddModelError("VenueId", "Invalid VenueId provided.");
            }

            // Check for duplicate EventId
            if (_context.Events.Any(e => e.EventId == @event.EventId))
            {
                ModelState.AddModelError("EventId", "This EventId already exists. Please use a unique ID.");
            }

            if (ModelState.IsValid)
            {
                _context.Add(@event);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(@event); // Show form with errors
        }


        // GET: Events/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events.FindAsync(id);
            if (@event == null)
            {
                return NotFound();
            }
            return View(@event); // Pass the event to the Edit view
        }

        // POST: Events/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EventId,Name,EventDate,VenueId,Description,ImageUrl")] Event @event)
        {
            if (id != @event.EventId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@event);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventExists(@event.EventId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(@event); // Return the view if the model state is invalid
        }

        // GET: Events/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @event = await _context.Events
                .Include(e => e.Venue)
                .FirstOrDefaultAsync(m => m.EventId == id);
            if (@event == null)
            {
                return NotFound();
            }

            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @event = await _context.Events.FindAsync(id);
            if (@event != null)
            {
                _context.Events.Remove(@event);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventExists(int id)
        {
            return _context.Events.Any(e => e.EventId == id);
        }
    }
}
