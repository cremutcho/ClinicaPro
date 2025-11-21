using MediatR;
using ClinicaPro.Core.Entities;

namespace ClinicaPro.Core.Features.Funcionarios.Queries.GetFuncionariosPorCargo
{
    public class GetFuncionariosPorCargoQuery : IRequest<IEnumerable<Funcionario>>
    {
        public int CargoId { get; }

        public GetFuncionariosPorCargoQuery(int cargoId)
        {
            CargoId = cargoId;
        }
    }
}
