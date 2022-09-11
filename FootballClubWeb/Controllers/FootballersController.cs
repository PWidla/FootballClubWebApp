using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FootballClubWeb.Data;
using FootballClubWeb.Models;
using Microsoft.AspNetCore.Authorization;

namespace FootballClubWeb.Controllers
{
    public class FootballersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FootballersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Footballers
        public async Task<IActionResult> Index()
        {
              return _context.Footballer != null ? 
                          View(await _context.Footballer.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Footballer'  is null.");
        }


        // GET: Footballers/ShowSearchForm
        public async Task<IActionResult> ShowSearchForm()
        {
            return _context.Footballer != null ?
                        View() :
                        Problem("Entity set 'ApplicationDbContext.Footballer'  is null.");
        }

        // POST: Footballers/ShowSearchResults
        public async Task<IActionResult> ShowSearchResults(String SearchPhrase)
        {
            return _context.Footballer != null ?
                        View("Index", await _context.Footballer.Where(j => (j.Name.Contains(SearchPhrase) || j.Surname.Contains(SearchPhrase))).ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Footballer'  is null.");
        }

        // GET: Footballers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Footballer == null)
            {
                return NotFound();
            }

            var footballer = await _context.Footballer
                .FirstOrDefaultAsync(m => m.Id == id);
            if (footballer == null)
            {
                return NotFound();
            }

            return View(footballer);
        }

        // GET: Footballers/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Footballers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Surname,Position")] Footballer footballer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(footballer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(footballer);
        }

        // GET: Footballers/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Footballer == null)
            {
                return NotFound();
            }

            var footballer = await _context.Footballer.FindAsync(id);
            if (footballer == null)
            {
                return NotFound();
            }
            return View(footballer);
        }

        // POST: Footballers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Surname,Position")] Footballer footballer)
        {
            if (id != footballer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(footballer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FootballerExists(footballer.Id))
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
            return View(footballer);
        }

        // GET: Footballers/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Footballer == null)
            {
                return NotFound();
            }

            var footballer = await _context.Footballer
                .FirstOrDefaultAsync(m => m.Id == id);
            if (footballer == null)
            {
                return NotFound();
            }

            return View(footballer);
        }

        // POST: Footballers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Footballer == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Footballer'  is null.");
            }
            var footballer = await _context.Footballer.FindAsync(id);
            if (footballer != null)
            {
                _context.Footballer.Remove(footballer);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FootballerExists(int id)
        {
          return (_context.Footballer?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
