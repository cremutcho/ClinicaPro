using MediatR;
using ClinicaPro.Core.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace ClinicaPro.Core.Features.Funcionarios.Commands
{
    public class AtualizarFuncionarioCommandHandler : IRequestHandler<AtualizarFuncionarioCommand, Unit>
    {
        private readonly IFuncionarioRepository _repository;

        public AtualizarFuncionarioCommandHandler(IFuncionarioRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(AtualizarFuncionarioCommand request, CancellationToken cancellationToken)
        {
            // 1. Busque a entidade existente
            var funcionario = await _repository.GetByIdAsync(request.Id);

            if (funcionario == null)
            {
                // Em um projeto real, isso lançaria uma exceção HTTP 404/NotFound
                throw new ApplicationException($"Funcionário com ID {request.Id} não encontrado.");
            }

            // 2. Mapeie os novos dados para a entidade
            funcionario.Nome = request.Nome;
            funcionario.Sobrenome = request.Sobrenome;
            funcionario.CPF = request.CPF;
            funcionario.DataContratacao = request.DataContratacao;
            funcionario.Cargo = request.Cargo;
            funcionario.Ativo = request.Ativo;

            // 3. Salve as mudanças
            await _repository.UpdateAsync(funcionario); 
            
            return Unit.Value;
        }
    }
}