using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ClinicaPro.Core.Entities;
using ClinicaPro.Infrastructure.Data;

namespace ClinicaPro.Web.Controllers
{
    public class ConsultaController : Controller
    {
        private readonly ClinicaDbContext _context;

        public ConsultaController(ClinicaDbContext context)
        {
            _context = context;
        }

        // GET: Consulta
        public async Task<IActionResult> Index()
        {
            var consultas = _context.Consultas
                .Include(c => c.Paciente)
                .Include(c => c.Medico);
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

            return View(consulta);
        }

        // GET: Consulta/Create
        public IActionResult Create()
        {
            ViewBag.PacienteId = new SelectList(_context.Pacientes.ToList(), "Id", "Nome");
            ViewBag.MedicoId = new SelectList(_context.Medicos.ToList(), "Id", "Nome");
            return View();
        }

        // POST: Consulta/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DataHora,Status,PacienteId,MedicoId,Observacoes")] Consulta consulta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(consulta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.PacienteId = new SelectList(_context.Pacientes.ToList(), "Id", "Nome", consulta.PacienteId);
            ViewBag.MedicoId = new SelectList(_context.Medicos.ToList(), "Id", "Nome", consulta.MedicoId);
            return View(consulta);
        }

        // GET: Consulta/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var consulta = await _context.Consultas.FindAsync(id);
            if (consulta == null) return NotFound();

            ViewBag.PacienteId = new SelectList(_context.Pacientes.ToList(), "Id", "Nome", consulta.PacienteId);
            ViewBag.MedicoId = new SelectList(_context.Medicos.ToList(), "Id", "Nome", consulta.MedicoId);
            return View(consulta);
        }

        // POST: Consulta/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DataHora,Status,PacienteId,MedicoId,Observacoes")] Consulta consulta)
        {
            if (id != consulta.Id) return NotFound();

            if (ModelState.IsValid)
            {
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

            ViewBag.PacienteId = new SelectList(_context.Pacientes.ToList(), "Id", "Nome", consulta.PacienteId);
            ViewBag.MedicoId = new SelectList(_context.Medicos.ToList(), "Id", "Nome", consulta.MedicoId);
            return View(consulta);
        }

        // GET: Consulta/Delete/5
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
