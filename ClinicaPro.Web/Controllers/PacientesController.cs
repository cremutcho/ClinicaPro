using Microsoft.AspNetCore.Mvc;
using ClinicaPro.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using MediatR; 
using ClinicaPro.Core.Features.Pacientes.Queries;
using ClinicaPro.Core.Features.Pacientes.Commands; 

namespace ClinicaPro.Web.Controllers
{
    [Authorize(Roles = "Admin,Recepcionista")]
    public class PacientesController : Controller
    {
        private readonly IMediator _mediator; 

        public PacientesController(IMediator mediator) 
        {
            _mediator = mediator;
        }

        // GET: /Pacientes
        public async Task<IActionResult> Index()
        {
            var query = new ObterTodosPacientesQuery();
            var pacientes = await _mediator.Send(query);
            return View(pacientes);
        }

        // GET: /Pacientes/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var paciente = await _mediator.Send(new ObterPacientePorIdQuery { Id = id }); 
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

            // ✅ REFATORADO: Usa CriarPacienteCommand
            await _mediator.Send(new CriarPacienteCommand { Paciente = paciente });
            
            return RedirectToAction(nameof(Index));
        }

        // GET: /Pacientes/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var paciente = await _mediator.Send(new ObterPacientePorIdQuery { Id = id });
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
            
            // ✅ REFATORADO: Usa AtualizarPacienteCommand
            await _mediator.Send(new AtualizarPacienteCommand { Paciente = paciente });
            
            return RedirectToAction(nameof(Index));
        }

        // GET: /Pacientes/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var paciente = await _mediator.Send(new ObterPacientePorIdQuery { Id = id });
            if (paciente == null)
                return NotFound();
            return View(paciente);
        }

        // POST: /Pacientes/DeleteConfirmed/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // ✅ REFATORADO: Usa DeletarPacienteCommand
            await _mediator.Send(new DeletarPacienteCommand { Id = id });
            
            return RedirectToAction(nameof(Index));
        }
    }
}