using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ClinicaPro.Core.Features.Financeiro.ContasReceber.Queries
{
    public class GetAllContasReceberQueryHandler : IRequestHandler<GetAllContasReceberQuery, IEnumerable<ContaReceber>>
    {
        private readonly IContaReceberRepository _repository;

        public GetAllContasReceberQueryHandler(IContaReceberRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ContaReceber>> Handle(GetAllContasReceberQuery request, CancellationToken cancellationToken)
        {
            return await _repository.GetAllAsync();
        }
    }
}
