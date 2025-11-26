using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ClinicaPro.Core.Features.Financeiro.ContasPagar.Queries
{
    public class GetAllContasPagarQueryHandler : IRequestHandler<GetAllContasPagarQuery, IEnumerable<ContaPagar>>
    {
        private readonly IContaPagarRepository _repo;

        public GetAllContasPagarQueryHandler(IContaPagarRepository repo)
        {
            _repo = repo;
        }

        public async Task<IEnumerable<ContaPagar>> Handle(GetAllContasPagarQuery request, CancellationToken cancellationToken)
        {
            return await _repo.GetAllAsync();
        }
    }
}
