using MediatR;
using ClinicaPro.Core.Entities;

namespace ClinicaPro.Core.Features.Funcionarios.Queries.GetFuncionarioById
{
    public class GetFuncionarioByIdQuery : IRequest<Funcionario?>
    {
        public int Id { get; }

        public GetFuncionarioByIdQuery(int id)
        {
            Id = id;
        }
    }
}
