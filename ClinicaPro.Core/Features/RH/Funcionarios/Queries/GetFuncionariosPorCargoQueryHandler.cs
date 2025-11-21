using MediatR;
using ClinicaPro.Core.Interfaces;
using ClinicaPro.Core.Entities;

namespace ClinicaPro.Core.Features.Funcionarios.Queries.GetFuncionariosPorCargo
{
    public class GetFuncionariosPorCargoQueryHandler 
        : IRequestHandler<GetFuncionariosPorCargoQuery, IEnumerable<Funcionario>>
    {
        private readonly IFuncionarioRepository _repo;

        public GetFuncionariosPorCargoQueryHandler(IFuncionarioRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<Funcionario>> Handle(
            GetFuncionariosPorCargoQuery request,
            CancellationToken cancellationToken)
        {
            return await _repo.BuscarPorCargoAsync(request.CargoId);
        }
    }
}
