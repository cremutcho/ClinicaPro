using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using MediatR; // Adicionado para usar o IMediator

// Usings para Queries e Commands
using ClinicaPro.Core.Features.Medicos.Queries; 
using ClinicaPro.Core.Features.Medicos.Commands; // NOVO: Adicionado para usar o Command de Criação

// AVISO: A injeção direta de DbContext abaixo será removida completamente 
// em refatorações futuras, mas mantida por enquanto para que os outros 
// métodos (Details, Create (GET), Edit, Delete) continuem funcionando.
using ClinicaPro.Core.Entities;
using ClinicaPro.Infrastructure.Data;


namespace ClinicaPro.Web.Controllers
{
    [Authorize(Roles = "Admin,Medico")]
    public class MedicoController : Controller
    {
        // O _context é mantido temporariamente para os métodos não refatorados (GET, Edit, Delete).
        private readonly ClinicaDbContext _context; 

        // O IMediator está pronto para ser usado.
        private readonly IMediator _mediator; 

        public MedicoController(ClinicaDbContext context, IMediator mediator) // Construtor atualizado
        {
            _context = context;
            _mediator = mediator; 
        }

        // GET: Medico (Refatorado com MediatR/Query)
        public async Task<IActionResult> Index()
        {
            var query = new ObterTodosMedicosQuery();
            var medicos = await _mediator.Send(query); 
            return View(medicos);
        }

        // ✅ GET: Medico/Details/5 (Acesso direto ao DbContext mantido temporariamente)
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medico = await _context.Medicos
                .Include(m => m.Especialidade)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (medico == null)
            {
                return NotFound();
            }

            return View(medico);
        }

        // GET: Medico/Create (Acesso direto ao DbContext mantido temporariamente)
        // OBS: Idealmente, este método deveria retornar View(new CriarMedicoCommand())
        public IActionResult Create()
        {
            ViewBag.EspecialidadeId = new SelectList(_context.Especialidades.ToList(), "Id", "Nome");
            return View();
        }

        // POST: Medico/Create - REFATORADO PARA COMMAND
        [HttpPost]
        [ValidateAntiForgeryToken]
        // Recebe o DTO CriarMedicoCommand em vez da entidade Medico
        public async Task<IActionResult> Create(CriarMedicoCommand command) 
        {
            if (ModelState.IsValid)
            {
                // Envia o Command para o Handler via Mediator
                int novoMedicoId = await _mediator.Send(command);
                
                // Redireciona para a listagem (ou Details do novo item, se quisermos)
                return RedirectToAction(nameof(Index)); 
            }

            // Se houver erro de validação, recarrega a lista de especialidades
            // Este uso do _context será refatorado em breve (com outra Query/Command)
            ViewBag.EspecialidadeId = new SelectList(_context.Especialidades, "Id", "Nome", command.EspecialidadeId);
            return View(command); // Passa o Command de volta para a View para preservar os dados
        }

        // GET: Medico/Edit/5 (Acesso direto ao DbContext mantido temporariamente)
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var medico = await _context.Medicos.FindAsync(id);
            if (medico == null) return NotFound();

            ViewBag.EspecialidadeId = new SelectList(_context.Especialidades.ToList(), "Id", "Nome", medico.EspecialidadeId);
            return View(medico);
        }

        // POST: Medico/Edit/5 (Acesso direto ao DbContext mantido temporariamente)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,CRM,EspecialidadeId,Email,Telefone")] Medico medico)
        {
            if (id != medico.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medico);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicoExists(medico.Id)) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewBag.EspecialidadeId = new SelectList(_context.Especialidades.ToList(), "Id", "Nome", medico.EspecialidadeId);
            return View(medico);
        }

        // GET: Medico/Delete/5 (Acesso direto ao DbContext mantido temporariamente)
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var medico = await _context.Medicos
                .Include(m => m.Especialidade)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (medico == null) return NotFound();

            return View(medico);
        }

        // POST: Medico/Delete/5 (Acesso direto ao DbContext mantido temporariamente)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var medico = await _context.Medicos.FindAsync(id);
            if (medico != null)
                _context.Medicos.Remove(medico);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Função auxiliar que ainda usa o DbContext
        private bool MedicoExists(int id)
        {
            return _context.Medicos.Any(e => e.Id == id);
        }
    }
}