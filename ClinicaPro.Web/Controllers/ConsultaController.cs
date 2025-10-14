using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClinicaPro.Core.Entities;
using ClinicaPro.Infrastructure.Data;

namespace ClinicaPro.Web.Controllers
{
    [Authorize(Roles = "Admin,Medico,Recepcionista")]
    public class ConsultaController : Controller
    {
        private readonly ClinicaDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ConsultaController(ClinicaDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Consulta
        public async Task<IActionResult> Index()
        {
            var consultas = _context.Consultas
                .Include(c => c.Paciente)
                .Include(c => c.Medico)
                .AsQueryable();

            // Se for médico, mostra apenas consultas do próprio médico
            if (User.IsInRole("Medico"))
            {
                var user = await _userManager.GetUserAsync(User);
                consultas = consultas.Where(c => c.Medico.Email == user.Email);
            }

            return View(await consultas.ToListAsync());
        }

        // GET: Consulta/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var consulta = await _context.Consultas
                .Include(c => c.Paciente)
                .Include(c => c.Medico)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (consulta == null) return NotFound();

            // Médicos só podem ver suas próprias consultas
            if (User.IsInRole("Medico"))
            {
                var user = await _userManager.GetUserAsync(User);
                if (consulta.Medico.Email != user.Email)
                    return Forbid();
            }

            return View(consulta);
        }

        // GET: Consulta/Create
        [Authorize(Roles = "Admin,Recepcionista")]
        public IActionResult Create()
        {
            ViewBag.PacienteId = new SelectList(_context.Pacientes, "Id", "Nome");
            ViewBag.MedicoId = new SelectList(_context.Medicos, "Id", "Nome");
            return View();
        }

        // POST: Consulta/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Recepcionista")]
        public async Task<IActionResult> Create([Bind("Id,DataHora,Status,PacienteId,MedicoId,Observacoes")] Consulta consulta)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.PacienteId = new SelectList(_context.Pacientes, "Id", "Nome", consulta.PacienteId);
                ViewBag.MedicoId = new SelectList(_context.Medicos, "Id", "Nome", consulta.MedicoId);
                return View(consulta);
            }

            _context.Add(consulta);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Consulta/Edit/5
        [Authorize(Roles = "Admin,Recepcionista")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var consulta = await _context.Consultas.FindAsync(id);
            if (consulta == null) return NotFound();

            ViewBag.PacienteId = new SelectList(_context.Pacientes, "Id", "Nome", consulta.PacienteId);
            ViewBag.MedicoId = new SelectList(_context.Medicos, "Id", "Nome", consulta.MedicoId);
            return View(consulta);
        }

        // POST: Consulta/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Recepcionista")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DataHora,Status,PacienteId,MedicoId,Observacoes")] Consulta consulta)
        {
            if (id != consulta.Id) return NotFound();

            if (!ModelState.IsValid)
            {
                ViewBag.PacienteId = new SelectList(_context.Pacientes, "Id", "Nome", consulta.PacienteId);
                ViewBag.MedicoId = new SelectList(_context.Medicos, "Id", "Nome", consulta.MedicoId);
                return View(consulta);
            }

            try
            {
                _context.Update(consulta);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ConsultaExists(consulta.Id)) return NotFound();
                else throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Consulta/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var consulta = await _context.Consultas
                .Include(c => c.Paciente)
                .Include(c => c.Medico)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (consulta == null) return NotFound();

            return View(consulta);
        }

        // POST: Consulta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var consulta = await _context.Consultas.FindAsync(id);
            if (consulta != null)
            {
                _context.Consultas.Remove(consulta);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ConsultaExists(int id)
        {
            return _context.Consultas.Any(e => e.Id == id);
        }
    }
}
