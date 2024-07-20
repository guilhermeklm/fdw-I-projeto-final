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
using Gerenciamento_eventos.Models.ViewModel;

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

        public async Task<IActionResult> Index(DateTime? data, int? localId, int? patrocinadorId)
        {
            var query = _context.Evento
                .Include(e => e.Local)
                .Include(e => e.Patrocinador)
                .Include(e => e.Inscricoes)
                .AsQueryable();

            if (data.HasValue)
            {
                if (data.Value.Date < DateTime.Now.Date)
                {
                    ModelState.AddModelError(string.Empty, "A data fornecida não pode ser anterior à data atual.");
                    ViewBag.Locais = _context.Local.ToList();
                    ViewBag.Patrocinadores = _context.Patrocinador.ToList();
                    return View(new List<EventoViewModel>());
                }

                query = query.Where(e => e.DataInicio.Date == data.Value.Date);
            } else {
                query = query.Where(e => e.DataInicio.Date >= DateTime.Now.Date)
                     .OrderBy(e => e.DataInicio);
            }

            if (localId.HasValue && localId.Value != 0)
            {
                query = query.Where(e => e.LocalId == localId.Value);
            }

            if (patrocinadorId.HasValue && patrocinadorId.Value != 0)
            {
                query = query.Where(e => e.PatrocinadorId == patrocinadorId.Value);
            }

            ViewBag.UserId = _userManager.GetUserAsync(User).Result.Id;
            var eventos = await query.ToListAsync();

            var eventosViewModel = eventos.Select(e => new EventoViewModel
            {
                Evento = e,
                InscricoesCount = e.Inscricoes.Count
            }).ToList();

            ViewBag.Locais = _context.Local.ToList();
            ViewBag.Patrocinadores = _context.Patrocinador.ToList();

            return View(eventosViewModel);
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
                .Include(e => e.Inscricoes)
                .ThenInclude(i => i.Participante)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (evento == null)
            {
                return NotFound();
            }

            var viewModel = new EventoViewModel
            {
                Evento = evento,
                InscricoesCount = evento.Inscricoes.Count,
                Participantes = evento.Inscricoes.Select(i => i.Participante).ToList()
            };

            ViewBag.UserId = _userManager.GetUserAsync(User).Result.Id;

            return View(viewModel);
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
        public async Task<IActionResult> Create([Bind("Id,Nome,Descricao,DataInicio,DataFim,LocalId,PatrocinadorId")] Evento evento)
        {
            var (isValid, mensagemErro) = await ValidarDatasEvento(evento);
            
            if (!isValid)
            {
                ModelState.AddModelError(string.Empty, mensagemErro);
                ViewData["LocalId"] = new SelectList(_context.Set<Local>(), "Id", "Nome");
                ViewData["PatrocinadorId"] = new SelectList(_context.Set<Patrocinador>(), "Id", "Nome");
                return View(evento);
            }

            if (evento.LocalId != 0) {
                evento.Local = _context.Local.Find(evento.LocalId);

                if (evento.Local == null) {
                    ModelState.AddModelError(string.Empty, "Local nao encontrado");
                    ViewData["LocalId"] = new SelectList(_context.Set<Local>(), "Id", "Nome");
                    ViewData["PatrocinadorId"] = new SelectList(_context.Set<Patrocinador>(), "Id", "Nome");
                    return View(evento);
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Local é obrigatorio");
                ViewData["LocalId"] = new SelectList(_context.Set<Local>(), "Id", "Nome");
                ViewData["PatrocinadorId"] = new SelectList(_context.Set<Patrocinador>(), "Id", "Nome");
                return View(evento);
            }

            if (evento.PatrocinadorId.HasValue && evento.PatrocinadorId.Value != 0)
            {
                evento.Patrocinador = await _context.Patrocinador.FindAsync(evento.PatrocinadorId.Value);
                if (evento.Patrocinador == null)
                {
                    ModelState.AddModelError(string.Empty, "Patrocinador não encontrado");
                    ViewData["LocalId"] = new SelectList(_context.Set<Local>(), "Id", "Nome");
                    ViewData["PatrocinadorId"] = new SelectList(_context.Set<Patrocinador>(), "Id", "Nome");
                    return View(evento);
                }
            }
            else
            {
                evento.PatrocinadorId = null;
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
                ModelState.AddModelError(string.Empty, "Usuario invalido");
                ViewData["LocalId"] = new SelectList(_context.Set<Local>(), "Id", "Nome");
                ViewData["PatrocinadorId"] = new SelectList(_context.Set<Patrocinador>(), "Id", "Nome");
                return View(evento);
            }

            _context.Add(evento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private async Task<(bool valido, string mensagemErro)> ValidarDatasEvento(Evento evento)
        {

            if (evento.DataFim <= evento.DataInicio)
            {
                return (false, "A data fim não pode ser antes ou igual à data de início.");
            }

            if ((evento.DataFim - evento.DataInicio).TotalHours < 1)
            {
                return (false, "A data fim deve ter no mínimo uma hora de diferença em relação à data de início.");
            }

            var eventoExistente = await _context.Evento
                .Where(e => e.LocalId == evento.LocalId &&
                            ((evento.DataInicio >= e.DataInicio && evento.DataInicio < e.DataFim) ||
                             (evento.DataFim > e.DataInicio && evento.DataFim <= e.DataFim)))
                .FirstOrDefaultAsync();

            if (eventoExistente != null)
            {
                return (false, "Já existe um evento neste local no mesmo horário.");
            }

            return (true, string.Empty);
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
                ViewData["LocalId"] = new SelectList(_context.Set<Local>(), "Id", "Nome", evento.LocalId);
                ViewData["PatrocinadorId"] = new SelectList(_context.Set<Patrocinador>(), "Id", "Nome", evento.PatrocinadorId);
                return NotFound();
            }

            ViewData["LocalId"] = new SelectList(_context.Set<Local>(), "Id", "Nome", evento.LocalId);
            ViewData["PatrocinadorId"] = new SelectList(_context.Set<Patrocinador>(), "Id", "Nome", evento.PatrocinadorId);
            return View(evento);
        }


        // POST: Evento/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Descricao,DataInicio,DataFim,LocalId,PatrocinadorId")] Evento evento)
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

            var (isValid, mensagemErro) = await ValidarDatasEvento(evento);

            if (!isValid)
            {
                ModelState.AddModelError(string.Empty, mensagemErro);
                ViewData["LocalId"] = new SelectList(_context.Set<Local>(), "Id", "Nome");
                ViewData["PatrocinadorId"] = new SelectList(_context.Set<Patrocinador>(), "Id", "Nome");
                return View(evento);
            }

            if (evento.PatrocinadorId.HasValue && evento.PatrocinadorId.Value != 0)
            {
                evento.Patrocinador = await _context.Patrocinador.FindAsync(evento.PatrocinadorId.Value);
                if (evento.Patrocinador == null)
                {
                    ModelState.AddModelError(string.Empty, "Patrocinador não encontrado");
                    ViewData["LocalId"] = new SelectList(_context.Set<Local>(), "Id", "Nome");
                    ViewData["PatrocinadorId"] = new SelectList(_context.Set<Patrocinador>(), "Id", "Nome");
                    return View(evento);
                }
            }
            else
            {
                evento.PatrocinadorId = null;
            }

            eventoSaved.Nome = evento.Nome;
            eventoSaved.LocalId = evento.LocalId;
            eventoSaved.PatrocinadorId = evento.PatrocinadorId;
            eventoSaved.DataInicio = evento.DataInicio;
            eventoSaved.DataFim = evento.DataFim;
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

            var participante = await _context.Participante.FirstOrDefaultAsync(p => p.UsuarioId == user.Id);

            if (participante == null)
            {
                participante = new Participante
                {
                    UsuarioId = user.Id,
                    Nome = user.Nome,
                    Email = user.Email
                };

                _context.Participante.Add(participante);
                await _context.SaveChangesAsync();
            }

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
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Desinscricao realizada com sucesso!";

            return RedirectToAction(nameof(Index));
        }
    }
}
