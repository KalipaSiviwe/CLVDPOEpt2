using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using POEPartOne.Models;

namespace POEPartOne.Controllers
{
    public class BookingsController : Controller
    {
        private readonly Poept1Context _context;

        public BookingsController(Poept1Context context)
        {
            _context = context;
        }

        // GET: Bookings
        public async Task<IActionResult> Index(string searchString)
        {
            var bookingDetails = from b in _context.BookingDetailsViews
                                 select b;

            ViewData["CurrentFilter"] = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                bookingDetails = bookingDetails.Where(b =>
                    b.BookingId.ToString().Contains(searchString) ||
                    b.EventName.Contains(searchString));
            }

            return View(await bookingDetails.ToListAsync());
        }

        // GET: Bookings/Create
        public IActionResult Create()
        {
            ViewData["EventId"] = new SelectList(_context.Events, "EventId", "Name");
            ViewData["VenueId"] = new SelectList(_context.Venues, "VenueId", "Name");
            return View();
        }

        // POST: Bookings/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BookingId,UserId,EventId,BookingsDate,VenueId")] Booking booking)
        {
            try
            {
                // Check if booking ID already exists
                if (await _context.Bookings.AnyAsync(b => b.BookingId == booking.BookingId))
                {
                    ModelState.AddModelError("BookingId", "This Booking ID already exists. Please use a different ID.");
                    goto PrepareViewData;
                }

                // Check if Event exists
                if (!await _context.Events.AnyAsync(e => e.EventId == booking.EventId))
                {
                    ModelState.AddModelError("EventId", "The selected Event does not exist.");
                    goto PrepareViewData;
                }

                // Check if Venue exists
                if (!await _context.Venues.AnyAsync(v => v.VenueId == booking.VenueId))
                {
                    ModelState.AddModelError("VenueId", "The selected Venue does not exist.");
                    goto PrepareViewData;
                }

                // Prevent double booking
                bool doubleBooked = await _context.Bookings.AnyAsync(b =>
                    b.VenueId == booking.VenueId &&
                    b.BookingsDate == booking.BookingsDate &&
                    b.BookingId != booking.BookingId);

                if (doubleBooked)
                {
                    ModelState.AddModelError("", "This venue is already booked for the selected date and time.");
                    goto PrepareViewData;
                }

                if (ModelState.IsValid)
                {
                    _context.Add(booking);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while saving: " + ex.Message);
                if (ex.InnerException != null)
                {
                    ModelState.AddModelError("", "Details: " + ex.InnerException.Message);
                }
            }

        PrepareViewData:
            ViewData["EventId"] = new SelectList(_context.Events, "EventId", "Name", booking.EventId);
            ViewData["VenueId"] = new SelectList(_context.Venues, "VenueId", "Name", booking.VenueId);
            return View(booking);
        }

        // GET: Bookings/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null) return NotFound();

            ViewData["EventId"] = new SelectList(_context.Events, "EventId", "Name", booking.EventId);
            ViewData["VenueId"] = new SelectList(_context.Venues, "VenueId", "Name", booking.VenueId);
            return View(booking);
        }

        // POST: Bookings/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BookingId,UserId,EventId,BookingsDate,VenueId")] Booking booking)
        {
            if (id != booking.BookingId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(booking);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingExists(booking.BookingId))
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

            ViewData["EventId"] = new SelectList(_context.Events, "EventId", "Name", booking.EventId);
            ViewData["VenueId"] = new SelectList(_context.Venues, "VenueId", "Name", booking.VenueId);
            return View(booking);
        }

        private bool BookingExists(int id)
        {
            return _context.Bookings.Any(e => e.BookingId == id);
        }
    }
}