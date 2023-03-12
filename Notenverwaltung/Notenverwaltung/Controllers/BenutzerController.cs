using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Notenverwaltung.Models;

namespace Notenverwaltung.Controllers
{
    public class BenutzerController : Controller
    {
        private readonly NotenverwaltungDB _context;

        public BenutzerController(NotenverwaltungDB context)
        {
            _context = context;
        }

        // GET: Benutzer
        public async Task<IActionResult> Index()
        {
              return _context.Benutzer != null ? 
                          View(await _context.Benutzer.ToListAsync()) :
                          Problem("Entity set 'NotenverwaltungDB.Benutzer'  is null.");
        }

        // GET: Benutzer/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Benutzer == null)
            {
                return NotFound();
            }

            var benutzer = await _context.Benutzer
                .FirstOrDefaultAsync(m => m.id == id);
            if (benutzer == null)
            {
                return NotFound();
            }

            return View(benutzer);
        }

        // GET: Benutzer/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Benutzer/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,benutzerName,benutzerPasswort")] Benutzer benutzer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(benutzer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(benutzer);
        }

        // GET: Benutzer/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Benutzer == null)
            {
                return NotFound();
            }

            var benutzer = await _context.Benutzer.FindAsync(id);
            if (benutzer == null)
            {
                return NotFound();
            }
            return View(benutzer);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> BenutzerErstellen([Bind("id,benutzerName,benutzerPasswort")] Benutzer benutzer)
        {
            if (ModelState.IsValid)
            {
                bool benutzerNameVergeben = false;
                _context.Benutzer.ToList().ForEach(x =>
                {
                    if (x.benutzerName == benutzer.benutzerName)
                    {
                        benutzerNameVergeben = true;
                    }
                });
                if (!benutzerNameVergeben)
                {
                    _context.Add(benutzer);
                    await _context.SaveChangesAsync();
                    TempData["BenutzerErstelltMessage"] = "Dein Benutzer wurde erfolgreich erstellt!";
                    return RedirectToAction("Index", "Home");
                }
            }
            TempData["BenutzerVergebenMessage"] = "Dieser Benutzer Name ist bereits vergeben!";
            return RedirectToAction("Create");
        }

        // POST: Benutzer/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,benutzerName,benutzerPasswort")] Benutzer benutzer)
        {
            if (id != benutzer.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(benutzer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BenutzerExists(benutzer.id))
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
            return View(benutzer);
        }

        // GET: Benutzer/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Benutzer == null)
            {
                return NotFound();
            }

            var benutzer = await _context.Benutzer
                .FirstOrDefaultAsync(m => m.id == id);
            if (benutzer == null)
            {
                return NotFound();
            }

            return View(benutzer);
        }

        // POST: Benutzer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Benutzer == null)
            {
                return Problem("Entity set 'NotenverwaltungDB.Benutzer'  is null.");
            }
            var benutzer = await _context.Benutzer.FindAsync(id);
            if (benutzer != null)
            {
                _context.Benutzer.Remove(benutzer);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BenutzerExists(int id)
        {
          return (_context.Benutzer?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
