using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MediatR;

using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Features.Consultas.Commands;
using ClinicaPro.Core.Features.Consultas.Queries;
using ClinicaPro.Core.Features.Medicos.Queries;
using ClinicaPro.Core.Features.Pacientes.Queries;
using ClinicaPro.Core.Features.Servicos.Queries;
using ClinicaPro.Core.Features.ConvenioMedico.Queries;
using ClinicaPro.Web.Models;
using Microsoft.EntityFrameworkCore;

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

        // ============================
        // GET: Consulta
        // ============================
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

        // ============================
        // GET: Consulta/Details/5
        // ============================
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var consulta = await _mediator.Send(new ObterConsultaPorIdQuery(id.Value));
            if (consulta == null) return NotFound();

            return View(consulta);
        }

        // ============================
        // GET: Consulta/Create
        // ============================
        [Authorize(Roles = "Admin,Recepcionista")]
        public async Task<IActionResult> Create()
        {
            var model = new ConsultaFormViewModel();
            await PopularDropdowns(model);
            return View(model);
        }

        // ============================
        // POST: Consulta/Create
        // ============================
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Recepcionista")]
        public async Task<IActionResult> Create(ConsultaFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await PopularDropdowns(model);
                return View(model);
            }

            var consulta = new Consulta
            {
                DataHora = model.DataHora,
                Status = model.Status,
                PacienteId = model.PacienteId,
                MedicoId = model.MedicoId,
                ServicoId = model.ServicoId,
                Observacoes = model.Observacoes,
                ConvenioMedicoId = model.TipoAtendimento == "Convenio" ? model.ConvenioMedicoId : null
            };

            try
            {
                await _mediator.Send(new CriarConsultaCommand { Consulta = consulta });
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError("DataHora", ex.Message);
                await PopularDropdowns(model);
                return View(model);
            }
        }

        // ============================
        // GET: Consulta/Edit/5
        // ============================
        [Authorize(Roles = "Admin,Recepcionista")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var consulta = await _mediator.Send(new ObterConsultaPorIdQuery(id.Value));
            if (consulta == null) return NotFound();

            var model = new ConsultaFormViewModel
            {
                Id = consulta.Id,
                DataHora = consulta.DataHora,
                Status = consulta.Status,
                PacienteId = consulta.PacienteId,
                MedicoId = consulta.MedicoId,
                ServicoId = consulta.ServicoId,
                Observacoes = consulta.Observacoes,
                ConvenioMedicoId = consulta.ConvenioMedicoId,
                TipoAtendimento = consulta.ConvenioMedicoId.HasValue ? "Convenio" : "Particular"
            };

            await PopularDropdowns(model);
            return View(model);
        }

        // ============================
        // POST: Consulta/Edit/5
        // ============================
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Recepcionista")]
        public async Task<IActionResult> Edit(int id, ConsultaFormViewModel model)
        {
            if (id != model.Id) return NotFound();

            if (!ModelState.IsValid)
            {
                await PopularDropdowns(model);
                return View(model);
            }

            // Buscar a entidade existente do banco
            var consultaExistente = await _mediator.Send(new ObterConsultaPorIdQuery(id));
            if (consultaExistente == null) return NotFound();

            // Atualizar os campos
            consultaExistente.DataHora = model.DataHora;
            consultaExistente.Status = model.Status;
            consultaExistente.PacienteId = model.PacienteId;
            consultaExistente.MedicoId = model.MedicoId;
            consultaExistente.ServicoId = model.ServicoId;
            consultaExistente.Observacoes = model.Observacoes;
            consultaExistente.ConvenioMedicoId = model.TipoAtendimento == "Convenio" ? model.ConvenioMedicoId : null;

            try
            {
                await _mediator.Send(new UpdateConsultaCommand { Consulta = consultaExistente });
                return RedirectToAction(nameof(Index));
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError("DataHora", ex.Message);
                await PopularDropdowns(model);
                return View(model);
            }
            catch (DbUpdateConcurrencyException)
            {
                var existe = await _mediator.Send(new ConsultaExisteQuery(model.Id));
                if (!existe) return NotFound();
                throw;
            }
        }

        // ============================
        // GET: Consulta/Delete/5
        // ============================
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var consulta = await _mediator.Send(new ObterConsultaPorIdQuery(id.Value));
            if (consulta == null) return NotFound();

            return View(consulta);
        }

        // ============================
        // POST: Consulta/Delete/5
        // ============================
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _mediator.Send(new DeleteConsultaCommand(id));
            return RedirectToAction(nameof(Index));
        }

        // ============================
        // MÉTODO AUXILIAR PARA DROPDOWNS
        // ============================
        private async Task PopularDropdowns(ConsultaFormViewModel? model = null)
        {
            var pacientes = await _mediator.Send(new ObterTodosPacientesQuery());
            var medicos = await _mediator.Send(new ObterTodosMedicosQuery());
            var servicos = await _mediator.Send(new ObterTodosServicosQuery());
            var convenios = await _mediator.Send(new GetAllConvenioMedicoQuery());

            ViewBag.PacienteId = new SelectList(pacientes, "Id", "Nome", model?.PacienteId);
            ViewBag.MedicoId = new SelectList(medicos, "Id", "Nome", model?.MedicoId);
            ViewBag.ServicoId = new SelectList(servicos, "Id", "Nome", model?.ServicoId);
            ViewBag.ConvenioMedicoId = new SelectList(convenios, "Id", "Nome", model?.ConvenioMedicoId);
        }
    }
}
