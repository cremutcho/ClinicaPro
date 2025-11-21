using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using System.Collections.Generic; // Para KeyNotFoundException
using ClinicaPro.Core.Entities;

// Usings para Queries e Commands
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
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var medico = await _mediator.Send(new ObterMedicoPorIdQuery(id.Value));
            if (medico == null) return NotFound();
            return View(medico);
        }

        // GET: Medico/Create
        public async Task<IActionResult> Create()
        {
            var especialidades = await _mediator.Send(new ObterTodasEspecialidadesQuery());
            ViewBag.EspecialidadeId = new SelectList(especialidades, "Id", "Nome");
            return View();
        }

        // POST: Medico/Create - CORREÇÃO FINAL BASEADA NO ERRO CS9035
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,CRM,EspecialidadeId,Email,Telefone")] Medico medico)
        {
            if (ModelState.IsValid)
            {
                // SOLUÇÃO DEFINITIVA: O compilador exige que a propriedade 'Medico' seja definida.
                var command = new CriarMedicoCommand 
                {
                    Medico = medico
                };

                await _mediator.Send(command);
                return RedirectToAction(nameof(Index));
            }

            var especialidades = await _mediator.Send(new ObterTodasEspecialidadesQuery());
            ViewBag.EspecialidadeId = new SelectList(especialidades, "Id", "Nome", medico.EspecialidadeId);
            return View(medico);
        }

        // GET: Medico/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var medico = await _mediator.Send(new ObterMedicoPorIdQuery(id.Value));
            if (medico == null) return NotFound();

            var especialidades = await _mediator.Send(new ObterTodasEspecialidadesQuery());
            ViewBag.EspecialidadeId = new SelectList(especialidades, "Id", "Nome", medico.EspecialidadeId);
            return View(medico);
        }

        // POST: Medico/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,CRM,EspecialidadeId,Email,Telefone")] Medico medico)
        {
            if (id != medico.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    // UpdateMedicoCommand mantido com inicializador de objeto
                    var command = new UpdateMedicoCommand
                    {
                        Id = medico.Id,
                        Nome = medico.Nome,
                        CRM = medico.CRM,
                        EspecialidadeId = medico.EspecialidadeId,
                        Email = medico.Email,
                        Telefone = medico.Telefone
                    };
                    await _mediator.Send(command);
                }
                catch (KeyNotFoundException)
                {
                    return NotFound();
                }
                catch (DbUpdateConcurrencyException)
                {
                    var existe = await _mediator.Send(new MedicoExisteQuery(medico.Id));
                    if (!existe) return NotFound();
                    else throw;
                }
                return RedirectToAction(nameof(Index));
            }

            var especialidades = await _mediator.Send(new ObterTodasEspecialidadesQuery());
            ViewBag.EspecialidadeId = new SelectList(especialidades, "Id", "Nome", medico.EspecialidadeId);
            return View(medico);
        }

        // GET: Medico/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var medico = await _mediator.Send(new ObterMedicoPorIdQuery(id.Value));
            if (medico == null) return NotFound();

            return View(medico);
        }

        // POST: Medico/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _mediator.Send(new DeleteMedicoCommand(id));
            return RedirectToAction(nameof(Index));
        }
    }
}