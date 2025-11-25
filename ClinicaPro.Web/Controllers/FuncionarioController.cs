using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MediatR;

// Queries para relatórios via CQRS
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

        // ============================================================
        // CRUD Funcionario (Index / Details / Create / Edit / Delete)
        // ============================================================

        public async Task<IActionResult> Index()
        {
            var funcionarios = await _funcionarioRepository.GetAllAsync();
            return View(funcionarios);
        }

        public async Task<IActionResult> Details(int id)
        {
            var funcionario = await _funcionarioRepository.GetByIdAsync(id);
            if (funcionario == null) return NotFound();

            funcionario.Cargo = await _cargoRepository.GetByIdAsync(funcionario.CargoId);
            return View(funcionario);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Cargos = new SelectList(await _cargoRepository.GetAllAsync(), "Id", "Nome");
            return View();
        }

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

        public async Task<IActionResult> Edit(int id)
        {
            var funcionario = await _funcionarioRepository.GetByIdAsync(id);
            if (funcionario == null) return NotFound();

            ViewBag.Cargos = new SelectList(await _cargoRepository.GetAllAsync(), "Id", "Nome", funcionario.CargoId);
            return View(funcionario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Funcionario funcionario)
        {
            if (id != funcionario.Id) return BadRequest();

            if (ModelState.IsValid)
            {
                await _funcionarioRepository.UpdateAsync(funcionario);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Cargos = new SelectList(await _cargoRepository.GetAllAsync(), "Id", "Nome", funcionario.CargoId);
            return View(funcionario);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var funcionario = await _funcionarioRepository.GetByIdAsync(id);
            if (funcionario == null) return NotFound();

            funcionario.Cargo = await _cargoRepository.GetByIdAsync(funcionario.CargoId);
            return View(funcionario);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _funcionarioRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        // ============================================================
        // 🔥 RELATÓRIOS FUNCIONAIS (sem formulários de cadastro)
        // ============================================================

        // Relatório: Funcionários por Cargo
        // GET & POST: Funcionario/PorCargo
        [HttpGet]
        public async Task<IActionResult> PorCargo()
        {
            // Exibe o dropdown de cargos para o usuário escolher
            ViewBag.Cargos = new SelectList(await _cargoRepository.GetAllAsync(), "Id", "Nome");
            return View(); // Procura automaticamente PorCargo.cshtml
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PorCargo(int? cargoId)
        {
            // Se não informar cargo, volta para o formulário com mensagem
            if (!cargoId.HasValue)
            {
                ViewBag.Cargos = new SelectList(await _cargoRepository.GetAllAsync(), "Id", "Nome");
                ModelState.AddModelError("cargoId", "Escolha um cargo.");
                return View(); // Ainda usa PorCargo.cshtml
            }

            // Busca os funcionários do cargo selecionado via MediatR
            var resultado = await _mediator.Send(new GetFuncionariosPorCargoQuery(cargoId.Value));

            // Reutiliza a mesma view PorCargo.cshtml ou, se quiser, uma view de listagem separada
            return View("PorCargo", resultado);
        }



        // Relatório: Funcionários por Status (Ativos/Inativos)
        public async Task<IActionResult> PorStatus(bool? ativo)
        {
            // Guarda o valor selecionado para preencher o dropdown
            ViewData["Ativo"] = ativo;

            if (!ativo.HasValue)
            {
                return View(); // mostra apenas o formulário
            }

            var resultado = await _mediator.Send(new GetFuncionariosPorStatusQuery(ativo.Value));

            return View(resultado);
        }



        // Relatório: Funcionários por Período
        public async Task<IActionResult> PorPeriodo(DateTime? inicio, DateTime? fim)
        {
            // Passa os valores para ViewData para preencher o formulário
            ViewData["Inicio"] = inicio?.ToString("yyyy-MM-dd");
            ViewData["Fim"] = fim?.ToString("yyyy-MM-dd");

            if (!inicio.HasValue || !fim.HasValue)
            {
                return View(); // apenas o formulário
            }

            var resultado = await _mediator.Send(
                new GetFuncionariosPorPeriodoQuery(inicio.Value, fim.Value)
            );

            return View(resultado);
        }


    }
}
