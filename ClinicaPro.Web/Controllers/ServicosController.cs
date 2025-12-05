using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MediatR;

using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Features.Servicos.Queries;
using ClinicaPro.Core.Features.Servicos.Commands;

namespace ClinicaPro.Web.Controllers
{
    [Authorize(Roles = "Admin,Recepcionista")]
    public class ServicosController : Controller
    {
        private readonly IMediator _mediator;

        public ServicosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: /Servicos
        public async Task<IActionResult> Index()
        {
            var servicos = await _mediator.Send(new ObterTodosServicosQuery());
            return View(servicos);
        }

        // GET: /Servicos/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var servico = await _mediator.Send(new ObterServicoPorIdQuery { Id = id });

            if (servico == null)
                return NotFound();

            return View(servico);
        }

        // GET: /Servicos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: /Servicos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Servico servico)
        {
            if (!ModelState.IsValid)
                return View(servico);

            await _mediator.Send(new CriarServicoCommand
            {
                Nome = servico.Nome,
                CodigoTuss = servico.CodigoTuss,
                ValorPadrao = servico.ValorPadrao
            });

            return RedirectToAction(nameof(Index));
        }

        // GET: /Servicos/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var servico = await _mediator.Send(new ObterServicoPorIdQuery { Id = id });

            if (servico == null)
                return NotFound();

            return View(servico);
        }

        // POST: /Servicos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Servico servico)
        {
            if (id != servico.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return View(servico);

            await _mediator.Send(new UpdateServicoCommand
            {
                Id = servico.Id,
                Nome = servico.Nome
            });

            return RedirectToAction(nameof(Index));
        }

        // GET: /Servicos/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var servico = await _mediator.Send(new ObterServicoPorIdQuery { Id = id });

            if (servico == null)
                return NotFound();

            return View(servico);
        }

        // POST: /Servicos/DeleteConfirmed/5
        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _mediator.Send(new DeleteServicoCommand { Id = id });

            return RedirectToAction(nameof(Index));
        }
    }
}
