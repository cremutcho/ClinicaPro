using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MediatR;

// 🔹 NOVOS USINGS — para os relatórios via CQRS
using ClinicaPro.Core.Features.Funcionarios.Queries.GetFuncionariosPorCargo;
using ClinicaPro.Core.Features.Funcionarios.Queries.GetFuncionariosPorStatus;
using ClinicaPro.Core.Features.Funcionarios.Queries.GetFuncionariosPorPeriodo;

namespace ClinicaPro.Web.Controllers
{
    [Authorize(Roles = "Admin,RH,Recepcionista")]
    public class FuncionarioController : Controller
    {
        private readonly IFuncionarioRepository _funcionarioRepository;
        private readonly ICargoRepository _cargoRepository;
        private readonly IMediator _mediator;

        public FuncionarioController(
            IFuncionarioRepository funcionarioRepository,
            ICargoRepository cargoRepository,
            IMediator mediator)
        {
            _funcionarioRepository = funcionarioRepository;
            _cargoRepository = cargoRepository;
            _mediator = mediator;
        }

        // GET: Funcionario
        public async Task<IActionResult> Index()
        {
            var funcionarios = await _funcionarioRepository.GetAllAsync();
            return View(funcionarios);
        }

        // GET: Funcionario/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var funcionario = await _funcionarioRepository.GetByIdAsync(id);

            if (funcionario == null)
                return NotFound();

            funcionario.Cargo = await _cargoRepository.GetByIdAsync(funcionario.CargoId);

            return View(funcionario);
        }

        // GET: Funcionario/Create
        public async Task<IActionResult> Create()
        {
            ViewBag.Cargos = new SelectList(await _cargoRepository.GetAllAsync(), "Id", "Nome");
            return View();
        }

        // POST: Funcionario/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Funcionario funcionario)
        {
            if (ModelState.IsValid)
            {
                await _funcionarioRepository.AddAsync(funcionario);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Cargos = new SelectList(await _cargoRepository.GetAllAsync(), "Id", "Nome");
            return View(funcionario);
        }

        // GET: Funcionario/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var funcionario = await _funcionarioRepository.GetByIdAsync(id);
            if (funcionario == null)
                return NotFound();

            ViewBag.Cargos = new SelectList(await _cargoRepository.GetAllAsync(), "Id", "Nome", funcionario.CargoId);

            return View(funcionario);
        }

        // POST: Funcionario/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Funcionario funcionario)
        {
            if (id != funcionario.Id)
                return BadRequest();

            if (ModelState.IsValid)
            {
                await _funcionarioRepository.UpdateAsync(funcionario);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Cargos = new SelectList(await _cargoRepository.GetAllAsync(), "Id", "Nome", funcionario.CargoId);
            return View(funcionario);
        }

        // GET: Funcionario/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var funcionario = await _funcionarioRepository.GetByIdAsync(id);

            if (funcionario == null)
                return NotFound();

            funcionario.Cargo = await _cargoRepository.GetByIdAsync(funcionario.CargoId);

            return View(funcionario);
        }

        // POST: Funcionario/DeleteConfirmed
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _funcionarioRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        // ============================================================
        // 🔥 NOVAS ROTAS DE RELATÓRIOS (Por Cargo / Status / Período)
        // ============================================================

        // GET: Funcionario/PorCargo
        [HttpGet]
        public async Task<IActionResult> PorCargo()
        {
            // monta o dropdown de cargos para o formulário
            ViewBag.Cargos = new SelectList(await _cargoRepository.GetAllAsync(), "Id", "Nome");
            return View(); // vai procurar Views/Funcionario/PorCargo.cshtml
        }

        // POST: Funcionario/PorCargo
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PorCargo(int? cargoId)
        {
            // Se o usuário submeter sem escolher cargo, redireciona de volta ao formulário
            if (!cargoId.HasValue)
            {
                ViewBag.Cargos = new SelectList(await _cargoRepository.GetAllAsync(), "Id", "Nome");
                ModelState.AddModelError("cargoId", "Escolha um cargo.");
                return View();
            }

            // Busca via MediatR (sua query espera int)
            var resultado = await _mediator.Send(new GetFuncionariosPorCargoQuery(cargoId.Value));

            // reutiliza a View Index para mostrar a lista filtrada
            return View("Index", resultado);
        }

        // GET: Funcionario/PorStatus?ativo=true
        public async Task<IActionResult> PorStatus(bool ativo)
        {
            var resultado = await _mediator.Send(new GetFuncionariosPorStatusQuery(ativo));
            return View("Index", resultado);
        }

        // GET: Funcionario/PorPeriodo?inicio=2024-01-01&fim=2024-12-31
        public async Task<IActionResult> PorPeriodo(DateTime inicio, DateTime fim)
        {
            var resultado = await _mediator.Send(new GetFuncionariosPorPeriodoQuery(inicio, fim));
            return View("Index", resultado);
        }
    }
}
