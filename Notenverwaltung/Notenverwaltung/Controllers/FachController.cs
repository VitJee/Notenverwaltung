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
    public class FachController : Controller
    {
        private readonly NotenverwaltungDB _context;

        public FachController(NotenverwaltungDB context)
        {
            _context = context;
        }

        // GET: Fach
        public async Task<IActionResult> Index()
        {
            DatenViewModel.instance.initialisiereDB(_context);
            return _context.Fach != null ? 
                          View(await _context.Fach.ToListAsync()) :
                          Problem("Entity set 'NotenverwaltungDB.Fach'  is null.");
        }

        // GET: Fach/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null || _context.Fach == null)
            {
                return NotFound();
            }

            var fach = await _context.Fach
                .FirstOrDefaultAsync(m => m.id == id);
            if (fach == null)
            {
                return NotFound();
            }

            DatenViewModel.instance.fachId = fach.id;
            DatenViewModel.instance.fachName = fach.fachName;

            return RedirectToAction("Index", "Note");
        }

        // GET: Fach/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Fach/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,fachName,benutzerId")] Fach fach)
        {
            if (ModelState.IsValid)
            {
                fach.benutzerId = DatenViewModel.instance.benutzerId;
                _context.Add(fach);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fach);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FachHinzufuegen([Bind("id,fachName,benutzerId")] Fach fach)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fach);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fach);
        }

        // GET: Fach/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Fach == null)
            {
                return NotFound();
            }

            var fach = await _context.Fach.FindAsync(id);
            if (fach == null)
            {
                return NotFound();
            }
            return View(fach);
        }

        // POST: Fach/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,fachName,benutzerId")] Fach fach)
        {
            if (id != fach.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    fach.benutzerId = DatenViewModel.instance.benutzerId;
                    _context.Update(fach);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FachExists(fach.id))
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
            return View(fach);
        }

        // GET: Fach/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Fach == null)
            {
                return NotFound();
            }

            var fach = await _context.Fach
                .FirstOrDefaultAsync(m => m.id == id);
            if (fach == null)
            {
                return NotFound();
            }

            return View(fach);
        }

        // POST: Fach/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Fach == null)
            {
                return Problem("Entity set 'NotenverwaltungDB.Fach'  is null.");
            }
            var fach = await _context.Fach.FindAsync(id);
            if (fach != null)
            {
                _context.Fach.Remove(fach);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FachExists(int id)
        {
          return (_context.Fach?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
