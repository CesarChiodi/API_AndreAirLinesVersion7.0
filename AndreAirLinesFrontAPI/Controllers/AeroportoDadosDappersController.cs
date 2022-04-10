using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AndreAirLinesFrontAPI.Data;
using AndreAirLinesFrontAPI.Models;

namespace AndreAirLinesFrontAPI.Controllers
{
    public class AeroportoDadosDappersController : Controller
    {
        private readonly AndreAirLinesFrontAPIContext _context;

        public AeroportoDadosDappersController(AndreAirLinesFrontAPIContext context)
        {
            _context = context;
        }

        // GET: AeroportoDadosDappers
        public async Task<IActionResult> Index()
        {
            return View(await _context.AeroportoDadosDapper.ToListAsync());
        }

        // GET: AeroportoDadosDappers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aeroportoDadosDapper = await _context.AeroportoDadosDapper
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aeroportoDadosDapper == null)
            {
                return NotFound();
            }

            return View(aeroportoDadosDapper);
        }

        // GET: AeroportoDadosDappers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AeroportoDadosDappers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,City,Country,Code,Continent")] AeroportoDadosDapper aeroportoDadosDapper)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aeroportoDadosDapper);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(aeroportoDadosDapper);
        }

        // GET: AeroportoDadosDappers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aeroportoDadosDapper = await _context.AeroportoDadosDapper.FindAsync(id);
            if (aeroportoDadosDapper == null)
            {
                return NotFound();
            }
            return View(aeroportoDadosDapper);
        }

        // POST: AeroportoDadosDappers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,City,Country,Code,Continent")] AeroportoDadosDapper aeroportoDadosDapper)
        {
            if (id != aeroportoDadosDapper.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aeroportoDadosDapper);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AeroportoDadosDapperExists(aeroportoDadosDapper.Id))
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
            return View(aeroportoDadosDapper);
        }

        // GET: AeroportoDadosDappers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aeroportoDadosDapper = await _context.AeroportoDadosDapper
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aeroportoDadosDapper == null)
            {
                return NotFound();
            }

            return View(aeroportoDadosDapper);
        }

        // POST: AeroportoDadosDappers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var aeroportoDadosDapper = await _context.AeroportoDadosDapper.FindAsync(id);
            _context.AeroportoDadosDapper.Remove(aeroportoDadosDapper);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AeroportoDadosDapperExists(int id)
        {
            return _context.AeroportoDadosDapper.Any(e => e.Id == id);
        }
    }
}
