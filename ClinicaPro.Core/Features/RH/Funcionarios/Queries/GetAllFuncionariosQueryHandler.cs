using MediatR;
using ClinicaPro.Core.Interfaces;
using ClinicaPro.Core.Entities;

namespace ClinicaPro.Core.Features.Funcionarios.Queries.GetAllFuncionarios
{
    public class GetAllFuncionariosQueryHandler 
        : IRequestHandler<GetAllFuncionariosQuery, IEnumerable<Funcionario>>
    {
        private readonly IFuncionarioRepository _repo;

        public GetAllFuncionariosQueryHandler(IFuncionarioRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Funcionario>> Handle(
            GetAllFuncionariosQuery request, 
            CancellationToken cancellationToken)
        {
            return await _repo.GetAllAsync();
        }
    }
}
