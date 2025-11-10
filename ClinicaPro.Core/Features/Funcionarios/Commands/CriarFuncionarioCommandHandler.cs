using MediatR;
using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace ClinicaPro.Core.Features.Funcionarios.Commands
{
    public class CriarFuncionarioCommandHandler : IRequestHandler<CriarFuncionarioCommand, int>
    {
        private readonly IFuncionarioRepository _repository;
        
        // No futuro, podemos injetar IValidator<CriarFuncionarioCommand> aqui se usarmos FluentValidation

        public CriarFuncionarioCommandHandler(IFuncionarioRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> Handle(CriarFuncionarioCommand request, CancellationToken cancellationToken)
        {
            // Mapeamento do Command para a Entidade
            var funcionario = new Funcionario
            {
                Nome = request.Nome,
                Sobrenome = request.Sobrenome,
                CPF = request.CPF,
                DataContratacao = request.DataContratacao,
                Cargo = request.Cargo,
                Ativo = request.Ativo
            };

            await _repository.AddAsync(funcionario);
            
            // Retorna o Id gerado (supondo que o AddAsync atribui o Id à entidade)
            return funcionario.Id; 
        }
    }
}