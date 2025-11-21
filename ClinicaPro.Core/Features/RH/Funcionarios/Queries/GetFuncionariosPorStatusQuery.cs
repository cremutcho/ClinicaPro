using MediatR;
using ClinicaPro.Core.Entities;

namespace ClinicaPro.Core.Features.Funcionarios.Queries.GetFuncionariosPorStatus
{
    public class GetFuncionariosPorStatusQuery : IRequest<IEnumerable<Funcionario>>
    {
        public bool Ativo { get; }

        public GetFuncionariosPorStatusQuery(bool ativo)
        {
            Ativo = ativo;
        }
    }
}
