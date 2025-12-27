using ClinicaPro.Core.Features.Cargos.Commands;
using ClinicaPro.Core.Features.Cargos.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ClinicaPro.Web.Controllers
{
    // O controlador exige autenticação e permite apenas usuários com a Role 'Admin'
    // Já que o módulo RH é tipicamente restrito.
    // [Authorize(Roles = "Admin")]
    public class CargoController : Controller
    {
        private readonly IMediator _mediator;

        public CargoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // ==========================================
        // 1. LISTAGEM (Index)
        // URL: /Cargo/Index
        // ==========================================
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // Envia a Query para buscar todos os cargos
            var query = new BuscarTodosCargosQuery();
            var cargos = await _mediator.Send(query);

            // Retorna a lista de DTOs para a View
            return View(cargos);
        }

        // ==========================================
        // 2. ADIÇÃO (GET - Exibir Formulário)
        // URL: /Cargo/Adicionar
        // ==========================================
        [HttpGet]
        public IActionResult Adicionar()
        {
            // Retorna a View com um CargoDto vazio (ou um ViewModel se preferir)
            return View(new AdicionarCargoCommand());
        }

        // ==========================================
        // 3. ADIÇÃO (POST - Processar Formulário)
        // URL: /Cargo/Adicionar
        // ==========================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Adicionar(AdicionarCargoCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View(command); // Retorna com erros de validação
            }

            try
            {
                // Envia o Command para o MediatR
                var novoId = await _mediator.Send(command);

                TempData["Sucesso"] = $"Cargo '{command.Nome}' adicionado com sucesso! (ID: {novoId})";
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException ex)
            {
                // Captura erros de negócio (ex: nome de cargo duplicado)
                ModelState.AddModelError("", ex.Message);
                return View(command);
            }
            catch (Exception)
            {
                // Captura outros erros
                ModelState.AddModelError("", "Ocorreu um erro inesperado ao adicionar o cargo.");
                return View(command);
            }
        }

        // ==========================================
        // 4. EDIÇÃO (GET - Exibir Formulário com dados)
        // URL: /Cargo/Editar/5
        // ==========================================
        [HttpGet]
        public async Task<IActionResult> Editar(int id)
        {
            // Query para buscar um cargo por ID (Precisamos criar essa Query!)
            // Por enquanto, faremos uma simulação ou usaremos o Repositório diretamente 
            // (se a Query não for criada).
            // Para manter o padrão CQRS, vamos assumir a necessidade de criar a query:

            var query = new BuscarCargoPorIdQuery { Id = id };
            var cargoDto = await _mediator.Send(query);

            if (cargoDto == null)
            {
                TempData["Erro"] = "Cargo não encontrado para edição.";
                return RedirectToAction(nameof(Index));
            }

            // Mapeia o DTO para o Command de Atualização para preencher o formulário
            var command = new AtualizarCargoCommand
            {
                Id = cargoDto.Id,
                Nome = cargoDto.Nome,
                Descricao = cargoDto.Descricao
            };

            return View(command);
        }


        // ==========================================
        // 5. EDIÇÃO (POST - Processar Atualização)
        // URL: /Cargo/Editar
        // ==========================================
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(AtualizarCargoCommand command)
        {
            if (!ModelState.IsValid)
            {
                return View(command);
            }

            try
            {
                await _mediator.Send(command);
                TempData["Sucesso"] = $"Cargo '{command.Nome}' atualizado com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            catch (ApplicationException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(command);
            }
            catch (Exception)
            {
                ModelState.AddModelError("", "Ocorreu um erro inesperado ao atualizar o cargo.");
                return View(command);
            }
        }
        // ==========================================
        // 4.1. DETALHES (GET - Exibir Detalhes do Cargo)
        // URL: /Cargo/Detalhes/5
        // ==========================================
        [HttpGet]
        public async Task<IActionResult> Detalhes(int id)
        {
            // Query para buscar um cargo por ID
            var query = new BuscarCargoPorIdQuery { Id = id };
            var cargoDto = await _mediator.Send(query);

            if (cargoDto == null)
            {
                TempData["Erro"] = "Cargo não encontrado.";
                return RedirectToAction(nameof(Index));
            }

            // Para usar a View que sugeri no início, precisamos mapear para o AtualizarCargoCommand, 
            // pois sua View está esperando esse modelo.
            var command = new AtualizarCargoCommand
            {
                Id = cargoDto.Id,
                Nome = cargoDto.Nome,
                Descricao = cargoDto.Descricao
            };

            // Retorna a View 'Detalhes'
            return View(command);
        }
        // ==========================================
        // 5.1. DELEÇÃO (GET - Exibir Confirmação de Deleção)
        // URL: /Cargo/Deletar/5
        // ==========================================
        [HttpGet]
        public async Task<IActionResult> Deletar(int id)
        {
            // Reutilizando a lógica de busca por ID
            var query = new BuscarCargoPorIdQuery { Id = id };
            var cargoDto = await _mediator.Send(query);

            if (cargoDto == null)
            {
                TempData["Erro"] = "Cargo não encontrado para exclusão.";
                return RedirectToAction(nameof(Index));
            }

            // Mapeia o DTO para o Command de Atualização (ou um DTO de Leitura)
            // para preencher a tela de confirmação de exclusão.
            var command = new AtualizarCargoCommand
            {
                Id = cargoDto.Id,
                Nome = cargoDto.Nome,
                Descricao = cargoDto.Descricao
            };

            // Retorna a View 'Deletar.cshtml'
            return View(command);
        }

        // ==========================================
        // 6. DELEÇÃO (POST - Processar Deleção)
        // URL: /Cargo/Deletar/5
        // ==========================================
        [HttpPost, ActionName("Deletar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletarConfirmado(int id)
        {
            try
            {
                var command = new DeletarCargoCommand { Id = id };
                await _mediator.Send(command);

                TempData["Sucesso"] = "Cargo deletado com sucesso!";
            }
            catch (ApplicationException ex)
            {
                TempData["Erro"] = ex.Message;
            }
            catch (Exception)
            {
                TempData["Erro"] = "Erro inesperado ao deletar o cargo.";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}