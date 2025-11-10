using Microsoft.AspNetCore.Mvc;
using ClinicaPro.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using MediatR; 
using ClinicaPro.Core.Features.Funcionarios.Queries;
using ClinicaPro.Core.Features.Funcionarios.Commands; 
using System; // Necessário para ApplicationException e Exception
using System.Threading.Tasks; // Necessário para Task<IActionResult>
using System.Collections.Generic; // Necessário para IEnumerable

namespace ClinicaPro.Web.Controllers
{
    [Authorize] // Assumindo que o Controller exige autenticação
    public class FuncionarioController : Controller
    {
        private readonly IMediator _mediator;

        public FuncionarioController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // -----------------------------------------------------------------
        // AÇÕES DE LEITURA (INDEX/DETAILS)
        // -----------------------------------------------------------------

        // GET: /Funcionario/Index
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var funcionarios = await _mediator.Send(new ObterTodosFuncionariosQuery());
            return View(funcionarios);
        }
        
        // NOVO: GET: /Funcionario/Details/{id}
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            // 1. Envia a Query para buscar os dados
            var funcionario = await _mediator.Send(new ObterFuncionarioPorIdQuery(id.Value));

            if (funcionario == null)
            {
                return NotFound();
            }
            
            // 2. Retorna a Entidade para a View Details.cshtml
            return View(funcionario);
        }

        // -----------------------------------------------------------------
        // AÇÕES DE CRIAÇÃO (CREATE)
        // -----------------------------------------------------------------

        // GET: Exibe o formulário de criação
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Recebe os dados do formulário e envia o Command
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CriarFuncionarioCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View(command);
            }

            try
            {
                int novoId = await _mediator.Send(command);
                return RedirectToAction(nameof(Index)); 
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocorreu um erro ao salvar o funcionário: " + ex.Message);
                return View(command);
            }
        }
        
        // -----------------------------------------------------------------
        // AÇÕES DE EDIÇÃO (EDIT)
        // -----------------------------------------------------------------

        // GET: Exibe o formulário preenchido para edição
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            // 1. Envia a Query para buscar os dados
            var funcionario = await _mediator.Send(new ObterFuncionarioPorIdQuery(id.Value));

            if (funcionario == null)
            {
                return NotFound();
            }
            
            // 2. Mapeia os dados da Entidade para o Command de Edição
            var command = new AtualizarFuncionarioCommand(
                funcionario.Id,
                funcionario.Nome,
                funcionario.Sobrenome,
                funcionario.CPF,
                funcionario.DataContratacao,
                funcionario.Cargo,
                funcionario.Ativo
            );
            
            return View(command);
        }

        // POST: Recebe os dados editados e envia o Command de Atualização
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AtualizarFuncionarioCommand command)
        {
            if (id != command.Id)
            {
                return NotFound();
            }
            
            if (!ModelState.IsValid)
            {
                return View(command);
            }
            
            try
            {
                await _mediator.Send(command);
            }
            catch (ApplicationException)
            {
                return NotFound();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Ocorreu um erro ao atualizar o funcionário: " + ex.Message);
                return View(command);
            }
            
            return RedirectToAction(nameof(Index));
        }
        
        // -----------------------------------------------------------------
        // AÇÕES DE DELEÇÃO (DELETE)
        // -----------------------------------------------------------------
        
        // GET: Tela de Confirmação de Deleção
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            // Usa a Query para obter os dados para a tela de confirmação
            var funcionario = await _mediator.Send(new ObterFuncionarioPorIdQuery(id.Value));

            if (funcionario == null)
            {
                return NotFound();
            }
            
            // Retorna a Entidade para a View de confirmação
            return View(funcionario);
        }

        // POST: Executa o Command de Deleção
        [HttpPost, ActionName("Delete")] // ActionName é importante para diferenciar do GET
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _mediator.Send(new DeletarFuncionarioCommand(id));
            return RedirectToAction(nameof(Index));
        }
    }
}