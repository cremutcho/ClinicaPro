using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ClinicaPro.Core.Features.Financeiro.ContasPagar.Queries
{
    public class GetContaPagarByIdQueryHandler : IRequestHandler<GetContaPagarByIdQuery, ContaPagar>
    {
        private readonly IContaPagarRepository _repository;

        public GetContaPagarByIdQueryHandler(IContaPagarRepository repository)
        {
            _repository = repository;
        }

        public async Task<ContaPagar> Handle(GetContaPagarByIdQuery request, CancellationToken cancellationToken)
        {
            var conta = await _repository.GetByIdAsync(request.Id);
            return conta;
        }
    }
}
