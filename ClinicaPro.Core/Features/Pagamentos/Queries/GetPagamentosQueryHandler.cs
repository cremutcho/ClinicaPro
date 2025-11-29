using ClinicaPro.Core.Entities;
using ClinicaPro.Core.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ClinicaPro.Core.Features.Pagamentos.Queries
{
    public class GetPagamentosQueryHandler : IRequestHandler<GetPagamentosQuery, List<Pagamento>>
    {
        private readonly IPagamentoRepository _repository;

        public GetPagamentosQueryHandler(IPagamentoRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Pagamento>> Handle(GetPagamentosQuery request, CancellationToken cancellationToken)
        {
            var pagamentos = await _repository.GetAllAsync();
            return pagamentos.ToList(); // converte IReadOnlyList para List
        }
    }
}