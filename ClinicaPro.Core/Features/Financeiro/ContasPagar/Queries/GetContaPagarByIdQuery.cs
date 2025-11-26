using ClinicaPro.Core.Entities;
using MediatR;

namespace ClinicaPro.Core.Features.Financeiro.ContasPagar.Queries
{
    // Query para buscar uma ContaPagar pelo Id
    public class GetContaPagarByIdQuery : IRequest<ContaPagar>
    {
        public int Id { get; }

        public GetContaPagarByIdQuery(int id)
        {
            Id = id;
        }
    }
}
