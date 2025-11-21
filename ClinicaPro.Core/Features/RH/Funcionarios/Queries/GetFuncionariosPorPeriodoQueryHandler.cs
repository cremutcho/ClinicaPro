using MediatR;
using ClinicaPro.Core.Interfaces;
using ClinicaPro.Core.Entities;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ClinicaPro.Core.Features.Funcionarios.Queries.GetFuncionariosPorPeriodo
{
    public class GetFuncionariosPorPeriodoQueryHandler
        : IRequestHandler<GetFuncionariosPorPeriodoQuery, IEnumerable<Funcionario>>
    {
        private readonly IFuncionarioRepository _repo;

        public GetFuncionariosPorPeriodoQueryHandler(IFuncionarioRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Funcionario>> Handle(
            GetFuncionariosPorPeriodoQuery request,
            CancellationToken cancellationToken)
        {
            return await _repo.BuscarPorPeriodoAsync(
                request.DataInicio, 
                request.DataFim
            );
        }
    }
}
