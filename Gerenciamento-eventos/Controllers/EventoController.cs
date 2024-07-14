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
    public class EventoController : Controller
    {
        private readonly Gerenciamento_eventosContext _context;
        private readonly UserManager<Usuario> _userManager;

        public EventoController(Gerenciamento_eventosContext context, UserManager<Usuario> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Evento
        public async Task<IActionResult> Index()
        {
            var gerenciamento_eventosContext = _context.Evento
                .Include(e => e.Local)
                .Include(e => e.Patrocinador);

            ViewBag.UserId = _userManager.GetUserAsync(User).Result.Id;
            var eventos = await gerenciamento_eventosContext.ToListAsync();


            return View(eventos);
        }

        // GET: Evento/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Evento == null)
            {
                return NotFound();
            }

            var evento = await _context.Evento
                .Include(e => e.Local)
                .Include(e => e.Criador)
                .Include(e => e.Patrocinador)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (evento == null)
            {
                return NotFound();
            }

            return View(evento);
        }

        // GET: Evento/Create
        public IActionResult Create()
        {
            ViewData["LocalId"] = new SelectList(_context.Set<Local>(), "Id", "Nome");
            ViewData["PatrocinadorId"] = new SelectList(_context.Set<Patrocinador>(), "Id", "Nome");
            return View();
        }

        // POST: Evento/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Descricao,Data,LocalId,PatrocinadorId")] Evento evento)
        {
            if (evento.LocalId != 0) {
                evento.Local = _context.Local.Find(evento.LocalId);

                if (evento.Local == null) {
                    return BadRequest("Local nao encontrado");   
                }
            }
            else
            {
                return BadRequest("Local é obrigatorio");
            }

            if (evento.PatrocinadorId != 0)
            {
                evento.Patrocinador = _context.Patrocinador.Find(evento.PatrocinadorId);

                if (evento.Patrocinador == null)
                {
                    return BadRequest("Patrocinador nao encontrado");
                }
            } else {
                return BadRequest("Patrocinador é obrigatorio");
            }

            var user = _userManager.GetUserAsync(User);
            if (user != null)
            {
                evento.CriadorUsuarioId = user.Result.Id;
                evento.Criador = new Criador(
                    user.Result.Id, user.Result.Nome
                );
            }
            else
            {
                return BadRequest("Usuario invalido");
            }

            _context.Add(evento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Evento/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Evento == null)
            {
                return NotFound();
            }

            var evento = await _context.Evento.FindAsync(id);
            if (evento == null)
            {
                return NotFound();
            }

            ViewData["LocalId"] = new SelectList(_context.Set<Local>(), "Id", "Nome");
            ViewData["PatrocinadorId"] = new SelectList(_context.Set<Patrocinador>(), "Id", "Nome");
            return View(evento);
        }

        // POST: Evento/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Descricao,Data,LocalId,PatrocinadorId")] Evento evento)
        {
            if (id != evento.Id)
            {
                return NotFound("Evento nao encontrado");
            }

            var eventoSaved = _context.Evento.First(e => e.Id == id);

            if (eventoSaved == null)
            {
                return NotFound("Evento nao encontrado");
            }

            if (evento.PatrocinadorId != 0)
            {
                evento.Patrocinador = _context.Patrocinador.Find(evento.PatrocinadorId);

                if (evento.Patrocinador == null)
                {
                    return BadRequest("Patrocinador nao encontrado");
                }
            }
            else
            {
                return BadRequest("Patrocinador é obrigatorio");
            }

            eventoSaved.Nome = evento.Nome;
            eventoSaved.LocalId = evento.LocalId;
            eventoSaved.PatrocinadorId = evento.PatrocinadorId;
            eventoSaved.Data = evento.Data;
            eventoSaved.Descricao = evento.Descricao;

            await _context.SaveChangesAsync();

            ViewData["LocalId"] = new SelectList(_context.Set<Local>(), "Id", "Nome");
            ViewData["PatrocinadorId"] = new SelectList(_context.Set<Patrocinador>(), "Id", "Nome");
            return RedirectToAction(nameof(Index));
        }

        // GET: Evento/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Evento == null)
            {
                return NotFound();
            }

            var evento = await _context.Evento
                .Include(e => e.Local)
                .Include(e => e.Criador)
                .Include(e => e.Patrocinador)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (evento == null)
            {
                return NotFound();
            }

            return View(evento);
        }

        // POST: Evento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Evento == null)
            {
                return Problem("Entity set 'Gerenciamento_eventosContext.Evento'  is null.");
            }
            var evento = await _context.Evento.FindAsync(id);
            if (evento != null)
            {
                _context.Evento.Remove(evento);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool EventoExists(int id)
        {
          return (_context.Evento?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        // POST: Evento/Inscrever/6
        [HttpGet, ActionName("Inscricao")]
        public async Task<IActionResult> Inscrever(int? id)
        {
            var evento = await _context.Evento.FindAsync(id);
            if (evento == null)
            {
                return NotFound();
            }

            var user = _userManager.GetUserAsync(User).Result;

            var participante = new Participante();
            participante.UsuarioId = user.Id;
            participante.Nome = user.Nome;
            participante.Email = user.Email;
            participante.Eventos.Add(evento);

            _context.Participante.Add(participante);
            await _context.SaveChangesAsync();

            var inscricao = new Inscricao();
            inscricao.EventoId = id.Value;
            inscricao.DataInscricao = DateTime.Now;
            inscricao.ParticipanteUsuarioId = participante.UsuarioId;

            _context.Inscricao.Add(inscricao);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Inscrição realizada com sucesso!";

            return RedirectToAction(nameof(Index));
        }

        // GET: Evento/Desinscricao/6
        [HttpGet, ActionName("Desinscricao")]
        public async Task<IActionResult> Desinscricao(int? id)
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
            _context.Participante.Remove(participante);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Desinscricao realizada com sucesso!";

            return RedirectToAction(nameof(Index));
        }
    }
}
