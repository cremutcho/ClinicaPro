using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Exceptions;

// Queries e Commands
using ClinicaPro.Core.Features.Medicos.Queries;
using ClinicaPro.Core.Features.Medicos.Commands;
using ClinicaPro.Core.Features.Especialidades.Queries;

namespace ClinicaPro.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class MedicoController : Controller
    {
        private readonly IMediator _mediator;

        public MedicoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: Medico
        public async Task<IActionResult> Index()
        {
            var medicos = await _mediator.Send(new ObterTodosMedicosQuery());
            return View(medicos);
        }

        // GET: Medico/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var medico = await _mediator.Send(new ObterMedicoPorIdQuery(id));
            if (medico == null)
                return NotFound();

            return View(medico);
        }

        // GET: Medico/Create
        public async Task<IActionResult> Create()
        {
            var especialidades = await _mediator.Send(new ObterTodasEspecialidadesQuery());
            ViewBag.EspecialidadeId = new SelectList(especialidades, "Id", "Nome");
            return View();
        }

        // POST: Medico/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Medico medico)
        {
            if (!ModelState.IsValid)
            {
                await CarregarEspecialidades(medico.EspecialidadeId);
                return View(medico);
            }

            try
            {
                await _mediator.Send(new CriarMedicoCommand { Medico = medico });
                return RedirectToAction(nameof(Index));
            }
            catch (BusinessException ex)
            {
                // 🔔 CRM duplicado
                ModelState.AddModelError("CRM", ex.Message);
                await CarregarEspecialidades(medico.EspecialidadeId);
                return View(medico);
            }
        }

        // GET: Medico/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var medico = await _mediator.Send(new ObterMedicoPorIdQuery(id));
            if (medico == null)
                return NotFound();

            await CarregarEspecialidades(medico.EspecialidadeId);
            return View(medico);
        }

        // POST: Medico/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Medico medico)
        {
            if (id != medico.Id)
                return BadRequest();

            if (!ModelState.IsValid)
            {
                await CarregarEspecialidades(medico.EspecialidadeId);
                return View(medico);
            }

            try
            {
                await _mediator.Send(new UpdateMedicoCommand { Medico = medico });
                return RedirectToAction(nameof(Index));
            }
            catch (BusinessException ex)
            {
                // 🔔 CRM duplicado
                ModelState.AddModelError("CRM", ex.Message);
                await CarregarEspecialidades(medico.EspecialidadeId);
                return View(medico);
            }
        }

        // GET: Medico/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var medico = await _mediator.Send(new ObterMedicoPorIdQuery(id));
            if (medico == null)
                return NotFound();

            return View(medico);
        }

        // POST: Medico/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _mediator.Send(new DeleteMedicoCommand(id));
            return RedirectToAction(nameof(Index));
        }

        // ================================
        // 🔧 MÉTODO AUXILIAR
        // ================================
        private async Task CarregarEspecialidades(int? especialidadeId = null)
        {
            var especialidades = await _mediator.Send(new ObterTodasEspecialidadesQuery());
            ViewBag.EspecialidadeId =
                new SelectList(especialidades, "Id", "Nome", especialidadeId);
        }
    }
}
