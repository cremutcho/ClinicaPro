using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace ClinicaPro.Core.Features.RH.Queries
{
    // ----------------------------------------------------------------------
    // 1. QUERY (Requisição)
    // ----------------------------------------------------------------------
    // Retorna o FuncionarioDto (Modelo de Leitura) ou null se não for encontrado.
    public record ObterFuncionarioPorIdQuery(int Id) : IRequest<FuncionarioDto?>;

    // ----------------------------------------------------------------------
    // 2. HANDLER (Lógica de Busca)
    // ----------------------------------------------------------------------
    public class ObterFuncionarioPorIdQueryHandler : IRequestHandler<ObterFuncionarioPorIdQuery, FuncionarioDto?>
    {
        private readonly IFuncionarioRepository _funcionarioRepository;

        public ObterFuncionarioPorIdQueryHandler(IFuncionarioRepository funcionarioRepository)
        {
            _funcionarioRepository = funcionarioRepository;
        }

        public async Task<FuncionarioDto?> Handle(ObterFuncionarioPorIdQuery request, CancellationToken cancellationToken)
        {
            // Busca a entidade de domínio completa
            var funcionario = await _funcionarioRepository.GetByIdAsync(request.Id);

            if (funcionario == null)
            {
                // Retorna null, o Controller deve tratar o NotFound()
                return null; 
            }

            // Mapeamento da Entidade de Domínio para o DTO (Modelo de Leitura)
            var dto = new FuncionarioDto
            {
                Id = funcionario.Id,
                Nome = funcionario.Nome,
                Sobrenome = funcionario.Sobrenome,
                CPF = funcionario.CPF,
                Cargo = funcionario.Cargo,
                DataContratacao = funcionario.DataContratacao,
                Ativo = funcionario.Ativo
                // ATENÇÃO: Se DataNascimento e Salario existirem na entidade,
                // você precisará adicionar eles aqui para o DTO completo.
            };

            return dto;
        }
    }
}