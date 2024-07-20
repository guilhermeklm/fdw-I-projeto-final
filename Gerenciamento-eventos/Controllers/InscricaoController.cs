using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Gerenciamento_eventos.Data;
using Gerenciamento_eventos.Models;
using Microsoft.AspNetCore.Identity;
using Gerenciamento_eventos.Areas.Identity.Data;

namespace Gerenciamento_eventos.Controllers
{
    public class InscricaoController : Controller
    {
        private readonly Gerenciamento_eventosContext _context;
        private readonly UserManager<Usuario> _userManager;

        public InscricaoController(Gerenciamento_eventosContext context, UserManager<Usuario> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Inscricao
        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserAsync(User).Result.Id;
            var gerenciamento_eventosContext = _context.Inscricao
                .Include(i => i.Evento)
                .Include(i => i.Participante)
                .Where(i => i.ParticipanteUsuarioId == userId);

            var eventos = await _context.Evento
                .Include(e => e.Local)
                .ToListAsync();

            ViewBag.Eventos = eventos;

            return View(await gerenciamento_eventosContext.ToListAsync());
        }

        // GET: Inscricao/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Inscricao == null)
            {
                return NotFound();
            }

            var inscricao = await _context.Inscricao
                .Include(i => i.Evento)
                .Include(i => i.Participante)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (inscricao == null)
            {
                return NotFound();
            }

            return View(inscricao);
        }

        // POST: Inscricao/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Inscricao == null)
            {
                return Problem("Entity set 'Gerenciamento_eventosContext.Inscricao'  is null.");
            }
            var inscricao = await _context.Inscricao.FindAsync(id);
            if (inscricao != null)
            {
                _context.Inscricao.Remove(inscricao);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InscricaoExists(int id)
        {
          return (_context.Inscricao?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        // POST: Inscricao/Desinscricao/5
        [HttpPost, ActionName("Desinscricao")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DesinscricaoConfirmed(int id)
        {
            var evento = await _context.Evento.FindAsync(id);
            if (evento == null)
            {
                return NotFound();
            }

            var user = _userManager.GetUserAsync(User).Result;

            var participante = _context.Participante.First(p => p.UsuarioId == user.Id);
            var inscricao = _context.Inscricao.First(i => i.EventoId == id && i.ParticipanteUsuarioId == participante.UsuarioId);

            _context.Inscricao.Remove(inscricao);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Desinscricao realizada com sucesso!";

            return RedirectToAction(nameof(Index));
        }
    }
}
