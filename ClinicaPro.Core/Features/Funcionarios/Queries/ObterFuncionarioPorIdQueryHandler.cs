using MediatR;
using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace ClinicaPro.Core.Features.Funcionarios.Queries
{
    // O Handler retorna Funcionario?
    public class ObterFuncionarioPorIdQueryHandler : IRequestHandler<ObterFuncionarioPorIdQuery, Funcionario?>
    {
        private readonly IFuncionarioRepository _repository;

        public ObterFuncionarioPorIdQueryHandler(IFuncionarioRepository repository)
        {
            _repository = repository;
        }

        public async Task<Funcionario?> Handle(ObterFuncionarioPorIdQuery request, CancellationToken cancellationToken)
        {
            // Usa o método GetByIdAsync do seu repositório genérico
            return await _repository.GetByIdAsync(request.Id); 
        }
    }
}