using MediatR;
using ClinicaPro.Core.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace ClinicaPro.Core.Features.Funcionarios.Commands
{
    public class DeletarFuncionarioCommandHandler : IRequestHandler<DeletarFuncionarioCommand, Unit>
    {
        private readonly IFuncionarioRepository _repository;

        public DeletarFuncionarioCommandHandler(IFuncionarioRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(DeletarFuncionarioCommand request, CancellationToken cancellationToken)
        {
            // Usa o método DeleteAsync do repositório genérico
            await _repository.DeleteAsync(request.Id); 
            
            return Unit.Value;
        }
    }
}