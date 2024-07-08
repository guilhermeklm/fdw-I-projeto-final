using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Gerenciamento_eventos.Data;
using Gerenciamento_eventos.Models;

namespace Gerenciamento_eventos.Controllers
{
    public class PatrocinadorController : Controller
    {
        private readonly Gerenciamento_eventosContext _context;

        public PatrocinadorController(Gerenciamento_eventosContext context)
        {
            _context = context;
        }

        // GET: Patrocinador
        public async Task<IActionResult> Index()
        {
              return _context.Patrocinador != null ? 
                          View(await _context.Patrocinador.ToListAsync()) :
                          Problem("Entity set 'Gerenciamento_eventosContext.Patrocinador'  is null.");
        }

        // GET: Patrocinador/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Patrocinador == null)
            {
                return NotFound();
            }

            var patrocinador = await _context.Patrocinador
                .FirstOrDefaultAsync(m => m.Id == id);
            if (patrocinador == null)
            {
                return NotFound();
            }

            return View(patrocinador);
        }

        // GET: Patrocinador/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Patrocinador/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Email,Telefone")] Patrocinador patrocinador)
        {
            if (ModelState.IsValid)
            {
                _context.Add(patrocinador);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(patrocinador);
        }

        // GET: Patrocinador/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Patrocinador == null)
            {
                return NotFound();
            }

            var patrocinador = await _context.Patrocinador.FindAsync(id);
            if (patrocinador == null)
            {
                return NotFound();
            }
            return View(patrocinador);
        }

        // POST: Patrocinador/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Email,Telefone")] Patrocinador patrocinador)
        {
            if (id != patrocinador.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(patrocinador);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PatrocinadorExists(patrocinador.Id))
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
            return View(patrocinador);
        }

        // GET: Patrocinador/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Patrocinador == null)
            {
                return NotFound();
            }

            var patrocinador = await _context.Patrocinador
                .FirstOrDefaultAsync(m => m.Id == id);
            if (patrocinador == null)
            {
                return NotFound();
            }

            return View(patrocinador);
        }

        // POST: Patrocinador/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Patrocinador == null)
            {
                return Problem("Entity set 'Gerenciamento_eventosContext.Patrocinador'  is null.");
            }
            var patrocinador = await _context.Patrocinador.FindAsync(id);
            if (patrocinador != null)
            {
                _context.Patrocinador.Remove(patrocinador);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PatrocinadorExists(int id)
        {
          return (_context.Patrocinador?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
