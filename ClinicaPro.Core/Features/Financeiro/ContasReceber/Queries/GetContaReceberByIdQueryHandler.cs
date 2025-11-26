using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ClinicaPro.Core.Features.Financeiro.ContasReceber.Queries
{
    public class GetContaReceberByIdQueryHandler : IRequestHandler<GetContaReceberByIdQuery, ContaReceber>
    {
        private readonly IContaReceberRepository _repository;

        public GetContaReceberByIdQueryHandler(IContaReceberRepository repository)
        {
            _repository = repository;
        }

        public async Task<ContaReceber> Handle(GetContaReceberByIdQuery request, CancellationToken cancellationToken)
        {
            var conta = await _repository.GetByIdAsync(request.Id);
            return conta;
        }
    }
}
