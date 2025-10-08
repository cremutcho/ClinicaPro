using Microsoft.AspNetCore.Mvc;
using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Interfaces;
using ClinicaPro.Core.Services;
using Microsoft.AspNetCore.Authorization; // Caso IPacienteService esteja aqui

namespace ClinicaPro.Web.Controllers
{
    [Authorize(Roles = "Admin,Recepcionista")]
    public class PacientesController : Controller
    {
        private readonly IPacienteService _pacienteService;

        public PacientesController(IPacienteService pacienteService)
        {
            _pacienteService = pacienteService;
        }

        // GET: /Pacientes
        public async Task<IActionResult> Index()
        {

            var pacientes = await _pacienteService.GetAllAsync();
            return View(pacientes);
        }

        // GET: /Pacientes/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var paciente = await _pacienteService.GetByIdAsync(id);
            if (paciente == null)
                return NotFound();

            return View(paciente);
        }

        // GET: /Pacientes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Pacientes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Paciente paciente)
        {
            if (!ModelState.IsValid)
                return View(paciente);

            await _pacienteService.AddAsync(paciente);
            return RedirectToAction(nameof(Index));
        }

        // GET: /Pacientes/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var paciente = await _pacienteService.GetByIdAsync(id);
            if (paciente == null)
                return NotFound();

            return View(paciente);
        }

        // POST: /Pacientes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Paciente paciente)
        {
            if (id != paciente.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(paciente);

            await _pacienteService.UpdateAsync(paciente);
            return RedirectToAction(nameof(Index));
        }

        // GET: /Pacientes/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var paciente = await _pacienteService.GetByIdAsync(id);
            if (paciente == null)
                return NotFound();

            return View(paciente);
        }

        // POST: /Pacientes/DeleteConfirmed/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _pacienteService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
