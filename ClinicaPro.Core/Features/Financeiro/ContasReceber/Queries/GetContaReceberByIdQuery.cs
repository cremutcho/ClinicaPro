using ClinicaPro.Core.Entities;
using MediatR;

namespace ClinicaPro.Core.Features.Financeiro.ContasReceber.Queries
{
    public class GetContaReceberByIdQuery : IRequest<ContaReceber>
    {
        public int Id { get; }

        public GetContaReceberByIdQuery(int id)
        {
            Id = id;
        }
    }
}
