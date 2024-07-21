using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Reservation_Admin.Models;

namespace Reservation_Admin.Controllers
{
    public class MarkupsController : Controller
    {
        private readonly ReservationsContext _context;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration configuration;
        public MarkupsController(ReservationsContext context , IHttpClientFactory httpClientFactory, IConfiguration _configuration)
        {
            _context = context;
            _httpClientFactory = httpClientFactory;
            configuration = _configuration;
        }

        // GET: Markups
        public async Task<IActionResult> Index()
        {
            return View(await _context.FlightMarkups.ToListAsync());
        }

        // GET: Markups/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flightMarkup = await _context.FlightMarkups
                .FirstOrDefaultAsync(m => m.MarkupId == id);
            if (flightMarkup == null)
            {
                return NotFound();
            }

            return View(flightMarkup);
        }

        // GET: Markups/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Markups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MarkupId,AdultMarkup,ChildMarkup,InfantMarkup,ApplyMarkup,Airline,DiscountOnAirline,ApplyAirlineDiscount,Meta,DiscountOnMeta")] FlightMarkup flightMarkup)
        {
            if (ModelState.IsValid)
            {
                flightMarkup.CreatedOn = DateTime.Now;
                _context.Add(flightMarkup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(flightMarkup);
        }

        // GET: Markups/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flightMarkup = await _context.FlightMarkups.FindAsync(id);
            if (flightMarkup == null)
            {
                return NotFound();
            }
            return View(flightMarkup);
        }

        // POST: Markups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("MarkupId,AdultMarkup,ChildMarkup,InfantMarkup,ApplyMarkup,Airline,DiscountOnAirline,ApplyAirlineDiscount,Meta,DiscountOnMeta")] FlightMarkup flightMarkup)
        {
            if (id != flightMarkup.MarkupId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(flightMarkup);
                    await _context.SaveChangesAsync();
                    await applyChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FlightMarkupExists(flightMarkup.MarkupId))
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
            return View(flightMarkup);
        }

        // GET: Markups/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flightMarkup = await _context.FlightMarkups
                .FirstOrDefaultAsync(m => m.MarkupId == id);
            if (flightMarkup == null)
            {
                return NotFound();
            }

            return View(flightMarkup);
        }

        // POST: Markups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var flightMarkup = await _context.FlightMarkups.FindAsync(id);
            if (flightMarkup != null)
            {
                _context.FlightMarkups.Remove(flightMarkup);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FlightMarkupExists(long id)
        {
            return _context.FlightMarkups.Any(e => e.MarkupId == id);
        }

        private async Task applyChanges()
        {
            try
            {
                var ApiSettings = configuration.GetSection("API");
                var apiurl = ApiSettings["apiUrl"];
                var client = _httpClientFactory.CreateClient();
                var response = await client.GetAsync(apiurl+ "Availability/clearCache");
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    // Process the data
                }
            }
            catch
            {

            }
        }
    }
}
