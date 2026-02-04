using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Features.ConvenioMedico.Queries;
using ClinicaPro.Core.Features.ConvenioMedico.Commands;
using ClinicaPro.Core.Exceptions;

namespace ClinicaPro.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ConvenioMedicoController : Controller
    {
        private readonly IMediator _mediator;

        public ConvenioMedicoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: /ConvenioMedico
        public async Task<IActionResult> Index()
        {
            var convenios = await _mediator.Send(new GetAllConvenioMedicoQuery());
            return View(convenios);
        }

        // GET: /ConvenioMedico/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            var convenio = await _mediator.Send(new GetConvenioMedicoByIdQuery(id));
            if (convenio == null) return NotFound();
            return View(convenio);
        }

        // GET: /ConvenioMedico/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /ConvenioMedico/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ConvenioMedico convenio)
        {
            if (!ModelState.IsValid) return View(convenio);

            try
            {
                await _mediator.Send(new CriarConvenioMedicoCommand(convenio));
                return RedirectToAction(nameof(Index));
            }
            catch (BusinessException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(convenio);
            }
        }

        // GET: /ConvenioMedico/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var convenio = await _mediator.Send(new GetConvenioMedicoByIdQuery(id));
            if (convenio == null) return NotFound();
            return View(convenio);
        }

        // POST: /ConvenioMedico/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, ConvenioMedico convenio)
        {
            if (id != convenio.Id) return BadRequest();
            if (!ModelState.IsValid) return View(convenio);

            try
            {
                await _mediator.Send(new UpdateConvenioMedicoCommand(convenio));
                return RedirectToAction(nameof(Index));
            }
            catch (BusinessException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(convenio);
            }
        }

        // GET: /ConvenioMedico/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            var convenio = await _mediator.Send(new GetConvenioMedicoByIdQuery(id));
            if (convenio == null) return NotFound();
            return View(convenio);
        }

        // POST: /ConvenioMedico/DeleteConfirmed/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            await _mediator.Send(new DeletarConvenioMedicoCommand(id));
            return RedirectToAction(nameof(Index));
        }
    }
}
