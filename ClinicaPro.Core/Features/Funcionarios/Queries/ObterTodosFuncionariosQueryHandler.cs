using MediatR;
using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Interfaces; // Para usar IFuncionarioRepository
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ClinicaPro.Core.Features.Funcionarios.Queries
{
    public class ObterTodosFuncionariosQueryHandler : IRequestHandler<ObterTodosFuncionariosQuery, IEnumerable<Funcionario>>
    {
        private readonly IFuncionarioRepository _repository;

        public ObterTodosFuncionariosQueryHandler(IFuncionarioRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Funcionario>> Handle(ObterTodosFuncionariosQuery request, CancellationToken cancellationToken)
        {
            // Usa o método genérico para obter todos (Assumindo que GetTodosAsync existe no IRepository<T>)
            return await _repository.GetAllAsync(); 
        }
    }
}