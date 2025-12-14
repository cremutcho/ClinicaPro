using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MediatR;
using ClinicaPro.Core.Entities;
using System.Collections.Generic;

using ClinicaPro.Core.Features.Consultas.Queries;
using ClinicaPro.Core.Features.Consultas.Commands;
using ClinicaPro.Core.Features.Medicos.Queries;
using ClinicaPro.Core.Features.Pacientes.Queries;
using ClinicaPro.Core.Features.Servicos.Queries;

namespace ClinicaPro.Web.Controllers
{
    [Authorize(Roles = "Admin,Medico,Recepcionista")]
    public class ConsultaController : Controller
    {
        private readonly IMediator _mediator;
        private readonly UserManager<IdentityUser> _userManager;

        public ConsultaController(IMediator mediator, UserManager<IdentityUser> userManager)
        {
            _mediator = mediator;
            _userManager = userManager;
        }

        // GET: Consulta
        public async Task<IActionResult> Index()
        {
            var consultas = await _mediator.Send(new ObterTodasConsultasQuery()) ?? new List<Consulta>();

            if (User.IsInRole("Medico"))
            {
                var user = await _userManager.GetUserAsync(User);
                if (user != null)
                    consultas = consultas.Where(c => c.Medico != null && c.Medico.Email == user.Email).ToList();
            }

            return View(consultas);
        }

        // GET: Consulta/Details/5
        [Authorize(Roles = "Admin,Medico,Recepcionista")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var consulta = await _mediator.Send(new ObterConsultaPorIdQuery(id.Value));
            if (consulta == null) return NotFound();

            return View(consulta);
        }

        // GET: Consulta/Create
        [Authorize(Roles = "Admin,Recepcionista")]
        public async Task<IActionResult> Create()
        {
            await PopularDropdowns();
            return View();
        }

        // POST: Consulta/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Recepcionista")]
        public async Task<IActionResult> Create([Bind("Id,DataHora,Status,PacienteId,MedicoId,ServicoId,Observacoes")] Consulta consulta)
        {
            if (!ModelState.IsValid)
            {
                await PopularDropdowns(consulta);
                return View(consulta);
            }

            try
            {
                await _mediator.Send(new CriarConsultaCommand { Consulta = consulta });
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError("DataHora", ex.Message);
                await PopularDropdowns(consulta);
                return View(consulta);
            }
        }

        // GET: Consulta/Edit/5
        [Authorize(Roles = "Admin,Recepcionista")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var consulta = await _mediator.Send(new ObterConsultaPorIdQuery(id.Value));
            if (consulta == null) return NotFound();

            await PopularDropdowns(consulta);
            return View(consulta);
        }

        // POST: Consulta/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Recepcionista")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DataHora,Status,PacienteId,MedicoId,ServicoId,Observacoes")] Consulta consulta)
        {
            if (id != consulta.Id) return NotFound();

            if (!ModelState.IsValid)
            {
                await PopularDropdowns(consulta);
                return View(consulta);
            }

            try
            {
                await _mediator.Send(new UpdateConsultaCommand { Consulta = consulta });
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError("DataHora", ex.Message);
                await PopularDropdowns(consulta);
                return View(consulta);
            }
            catch (DbUpdateConcurrencyException)
            {
                var existe = await _mediator.Send(new ConsultaExisteQuery(consulta.Id));
                if (!existe) return NotFound();
                else throw;
            }
        }

        // GET: Consulta/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var consulta = await _mediator.Send(new ObterConsultaPorIdQuery(id.Value));
            if (consulta == null) return NotFound();

            return View(consulta);
        }

        // POST: Consulta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _mediator.Send(new DeleteConsultaCommand(id));
            return RedirectToAction(nameof(Index));
        }

        // Método auxiliar para popular dropdowns
        private async Task PopularDropdowns(Consulta? consulta = null)
        {
            var pacientes = await _mediator.Send(new ObterTodosPacientesQuery());
            var medicos = await _mediator.Send(new ObterTodosMedicosQuery());
            var servicos = await _mediator.Send(new ObterTodosServicosQuery());

            ViewBag.PacienteId = new SelectList(pacientes, "Id", "Nome", consulta?.PacienteId);
            ViewBag.MedicoId = new SelectList(medicos, "Id", "Nome", consulta?.MedicoId);
            ViewBag.ServicoId = new SelectList(servicos, "Id", "Nome", consulta?.ServicoId);
        }
    }
}
