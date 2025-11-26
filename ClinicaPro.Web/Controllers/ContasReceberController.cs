using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Features.Financeiro.ContasReceber.Commands;
using ClinicaPro.Core.Features.Financeiro.ContasReceber.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ClinicaPro.Web.Controllers
{
    public class ContasReceberController : Controller
    {
        private readonly IMediator _mediator;

        public ContasReceberController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // LISTA
        public async Task<IActionResult> Index()
        {
            var contas = await _mediator.Send(new GetAllContasReceberQuery());
            return View(contas);
        }

        // DETALHES
        public async Task<IActionResult> Details(int id)
        {
            var conta = await _mediator.Send(new GetContaReceberByIdQuery(id));
            if (conta == null)
                return NotFound();

            return View(conta);
        }

        // CREATE - GET
        public IActionResult Create()
        {
            return View();
        }

        // CREATE - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CriarContaReceberCommand command)
        {
            if (ModelState.IsValid)
            {
                await _mediator.Send(command);
                return RedirectToAction(nameof(Index));
            }
            return View(command.Conta);
        }

        // EDIT - GET
        public async Task<IActionResult> Edit(int id)
        {
            var conta = await _mediator.Send(new GetContaReceberByIdQuery(id));
            if (conta == null)
                return NotFound();

            return View(conta);
        }

        // EDIT - POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ContaReceber conta)
        {
            if (id != conta.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                await _mediator.Send(new UpdateContaReceberCommand(conta));
                return RedirectToAction(nameof(Index));
            }

            return View(conta);
        }

        // DELETE - GET
        public async Task<IActionResult> Delete(int id)
        {
            var conta = await _mediator.Send(new GetContaReceberByIdQuery(id));
            if (conta == null)
                return NotFound();

            return View(conta);
        }

        // DELETE - POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _mediator.Send(new DeleteContaReceberCommand(id));
            return RedirectToAction(nameof(Index));
        }
    }
}
