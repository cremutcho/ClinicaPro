using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Features.Pagamentos.Commands;
using ClinicaPro.Core.Features.Pagamentos.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace ClinicaPro.Web.Controllers
{
    [Route("Pagamentos")]
    public class PagamentosController : Controller
    {
        private readonly IMediator _mediator;

        public PagamentosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            var pagamentos = await _mediator.Send(new GetPagamentosQuery());
            return View(pagamentos);
        }

        [HttpGet("Create")]
        public IActionResult Create()
        {
            return View(new Pagamento());
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create(Pagamento pagamento)
        {
            if (!ModelState.IsValid)
                return View(pagamento);

            await _mediator.Send(new CriarPagamentoCommand(pagamento));

            return RedirectToAction(nameof(Index));
        }
    }
}
