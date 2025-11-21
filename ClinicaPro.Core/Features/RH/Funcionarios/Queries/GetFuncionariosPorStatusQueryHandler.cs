using MediatR;
using ClinicaPro.Core.Interfaces;
using ClinicaPro.Core.Entities;

namespace ClinicaPro.Core.Features.Funcionarios.Queries.GetFuncionariosPorStatus
{
    public class GetFuncionariosPorStatusQueryHandler
        : IRequestHandler<GetFuncionariosPorStatusQuery, IEnumerable<Funcionario>>
    {
        private readonly IFuncionarioRepository _repo;

        public GetFuncionariosPorStatusQueryHandler(IFuncionarioRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Funcionario>> Handle(
            GetFuncionariosPorStatusQuery request,
            CancellationToken cancellationToken)
        {
            return await _repo.BuscarPorStatusAsync(request.Ativo);
        }
    }
}
