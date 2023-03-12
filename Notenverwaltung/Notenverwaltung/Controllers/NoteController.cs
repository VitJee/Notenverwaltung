using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Notenverwaltung.Models;

namespace Notenverwaltung.Controllers
{
    public class NoteController : Controller
    {
        private readonly NotenverwaltungDB _context;

        public NoteController(NotenverwaltungDB context)
        {
            _context = context;
        }

        // GET: Note
        public async Task<IActionResult> Index()
        {
            DatenViewModel.instance.initialisiereDB(_context);
            return _context.Note != null ? 
                          View(await _context.Note.ToListAsync()) :
                          Problem("Entity set 'NotenverwaltungDB.Note'  is null.");
        }

        // GET: Note/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Note == null)
            {
                return NotFound();
            }

            var note = await _context.Note
                .FirstOrDefaultAsync(m => m.id == id);
            if (note == null)
            {
                return NotFound();
            }

            return View(note);
        }

        // GET: Note/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NoteHinzufuegen(int note, int gewichtung)
        {
            Note noteObjekt = new Note();
            if (ModelState.IsValid)
            {
                noteObjekt.fachId = DatenViewModel.instance.fachId;
                noteObjekt.note = note;
                noteObjekt.gewichtung = gewichtung;
                _context.Add(noteObjekt);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(noteObjekt);
        }

        // GET: Note/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Note == null)
            {
                return NotFound();
            }

            var note = await _context.Note.FindAsync(id);
            if (note == null)
            {
                return NotFound();
            }
            return View(note);
        }

        // POST: Note/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, int note, int gewichtung)
        {
            Note neueNote = new Note();
            neueNote.id = id;
            neueNote.note = note;
            neueNote.gewichtung = gewichtung;

            if (ModelState.IsValid)
            {
                try
                {
                    neueNote.fachId = DatenViewModel.instance.fachId;
                    _context.Update(neueNote);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NoteExists(neueNote.id))
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
            return View(note);
        }

        // GET: Note/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Note == null)
            {
                return NotFound();
            }

            var note = await _context.Note
                .FirstOrDefaultAsync(m => m.id == id);
            if (note == null)
            {
                return NotFound();
            }

            return View(note);
        }

        // POST: Note/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Note == null)
            {
                return Problem("Entity set 'NotenverwaltungDB.Note'  is null.");
            }
            var note = await _context.Note.FindAsync(id);
            if (note != null)
            {
                _context.Note.Remove(note);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NoteExists(int id)
        {
          return (_context.Note?.Any(e => e.id == id)).GetValueOrDefault();
        }

        public async Task<IActionResult> Zurueck()
        {
            return RedirectToAction("Index", "Fach");
        }
    }
}
