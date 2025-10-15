using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using MediatR; // NOVO: Adicionado para usar o IMediator

// NOVO: Adicione o using para a Query que criamos
using ClinicaPro.Core.Features.Medicos.Queries; 

// AVISO: A injeção direta de DbContext abaixo será removida completamente 
// em refatorações futuras, mas mantida por enquanto para que os outros 
// métodos (Details, Create, Edit, Delete) continuem funcionando.
using ClinicaPro.Core.Entities;
using ClinicaPro.Infrastructure.Data;


namespace ClinicaPro.Web.Controllers
{
    [Authorize(Roles = "Admin,Medico")]
    public class MedicoController : Controller
    {
        // REMOÇÃO: A injeção de ClinicaDbContext é uma má prática de arquitetura (violando Clean Arch)
        // Por enquanto, a mantemos para que os métodos POST e Edit funcionem.
        private readonly ClinicaDbContext _context; 

        // NOVO: Adicione o IMediator
        private readonly IMediator _mediator; 

        public MedicoController(ClinicaDbContext context, IMediator mediator) // Construtor atualizado
        {
            _context = context;
            _mediator = mediator; // O IMediator está pronto para ser usado
        }

        // GET: Medico (Método Refatorado com MediatR)
        public async Task<IActionResult> Index()
        {
            // O Contoller agora CRIA a Query (a requisição)
            var query = new ObterTodosMedicosQuery();
            
            // E envia a Query pelo Mediator. O Core/Handler faz o trabalho real.
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
        public IActionResult Create()
        {
            ViewBag.EspecialidadeId = new SelectList(_context.Especialidades.ToList(), "Id", "Nome");
            return View();
        }

        // POST: Medico/Create (Acesso direto ao DbContext mantido temporariamente)
        // ESTE É O PRÓXIMO ALVO DE REFATORAÇÃO (usando Commands)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,CRM,EspecialidadeId,Email,Telefone")] Medico medico)
        {
            if (ModelState.IsValid)
            {
                _context.Add(medico);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewBag.EspecialidadeId = new SelectList(_context.Especialidades, "Id", "Nome", medico.EspecialidadeId);
            return View(medico);
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